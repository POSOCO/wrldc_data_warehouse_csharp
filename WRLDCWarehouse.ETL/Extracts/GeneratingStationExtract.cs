using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class GeneratingStationExtract
    {
        public List<GeneratingStationForeign> ExtractGeneratingStationsForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT ID, GENERATING_STATION_NAME, CLASSIFICATION_ID, LOCATION_ID, STATION_TYPE, FUEL from GENERATING_STATION";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<GeneratingStationForeign> genStationsForeign = new List<GeneratingStationForeign>();
                        while (reader.Read())
                        {
                            GeneratingStationForeign genStationForeign = new GeneratingStationForeign();
                            genStationForeign.WebUatId = reader.GetInt32(0);
                            genStationForeign.Name = reader.GetString(1);
                            genStationForeign.GenClassificationWebUatId = reader.GetInt32(2);
                            genStationForeign.StateWebUatId = reader.GetInt32(3);
                            genStationForeign.GenerationTypeWebUatId = reader.GetInt32(4);
                            genStationForeign.FuelWebUatId = reader.GetInt32(5);
                            genStationsForeign.Add(genStationForeign);
                        }

                        reader.Dispose();

                        return genStationsForeign;
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
