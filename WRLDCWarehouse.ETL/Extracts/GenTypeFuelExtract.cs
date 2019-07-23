using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class GenTypeFuelExtract
    {
        public List<GenerationTypeFuelForeign> ExtractGenerationTypeFuelsForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, TYPE_ID, FUEL_ID from TYPE_FUEL_RELATION where :id=1 and TYPE_ID is NOT NULL and FUEL_ID is NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<GenerationTypeFuelForeign> genTypeFuelsForeign = new List<GenerationTypeFuelForeign>();
                        while (reader.Read())
                        {
                            GenerationTypeFuelForeign genTypeFuelForeign = new GenerationTypeFuelForeign();
                            genTypeFuelForeign.WebUatId = reader.GetInt32(0);
                            genTypeFuelForeign.GenerationTypeWebUatId = reader.GetInt32(1);
                            genTypeFuelForeign.FuelWebUatId = reader.GetInt32(2);
                            genTypeFuelsForeign.Add(genTypeFuelForeign);
                        }

                        reader.Dispose();

                        return genTypeFuelsForeign;
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
