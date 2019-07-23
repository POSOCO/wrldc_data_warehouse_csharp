using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class GeneratingStationOwnerExtract
    {
        public List<GeneratingStationOwnerForeign> ExtractGeneratingStationOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from ENTITY_ENTITY_RELN WHERE PARENT_ENTITY = 'GENERATING_STATION' AND PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='Owner' and CHILD_ENTITY_ATTRIBUTE = 'OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<GeneratingStationOwnerForeign> genStationOwnersForeign = new List<GeneratingStationOwnerForeign>();
                        while (reader.Read())
                        {
                            GeneratingStationOwnerForeign genStationOwnerForeign = new GeneratingStationOwnerForeign();
                            genStationOwnerForeign.WebUatId = reader.GetInt32(0);
                            genStationOwnerForeign.GeneratingStationWebUatId = reader.GetInt32(1);
                            genStationOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            genStationOwnersForeign.Add(genStationOwnerForeign);
                        }

                        reader.Dispose();

                        return genStationOwnersForeign;
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
