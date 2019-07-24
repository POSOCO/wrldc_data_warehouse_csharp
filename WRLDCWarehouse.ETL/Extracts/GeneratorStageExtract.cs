using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class GeneratorStageExtract
    {
        public List<GeneratorStageForeign> ExtractGeneratorStagesForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, STAGE_NAME, FK_GENERATING_STATION from STAGE where :id=1 and STAGE_NAME IS NOT NULL and ID IS NOT NULL and FK_GENERATING_STATION IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<GeneratorStageForeign> genStagesForeign = new List<GeneratorStageForeign>();
                        while (reader.Read())
                        {
                            GeneratorStageForeign genStageForeign = new GeneratorStageForeign();
                            genStageForeign.WebUatId = reader.GetInt32(0);
                            genStageForeign.Name = reader.GetString(1);
                            genStageForeign.GeneratingStationWebUatId = reader.GetInt32(2);
                            genStagesForeign.Add(genStageForeign);
                        }

                        reader.Dispose();

                        return genStagesForeign;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }
        }
    }
}
