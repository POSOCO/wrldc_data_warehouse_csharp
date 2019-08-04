using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class BayExtract
    {
        public List<BayForeign> ExtractBaysForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, VOLTAGE_ID, STATION_ID, BAY_TYPE_ID, SOURCE_ID, SOURCE_ENTITY, DESTINATION_ID, 
                                            DESTINATION_ENTITY, BAY_NUMBER, SOURCE_ENTITY_TYPE, DESTINATION_ENTITY_TYPE, 
                                            BAY_NAME from REPORTING_WEB_UI_UAT.BAY where :id=1";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<BayForeign> baysForeign = new List<BayForeign>();
                        while (reader.Read())
                        {
                            BayForeign bayForeign = new BayForeign();
                            bayForeign.WebUatId = reader.GetInt32(0);
                            bayForeign.VoltageWebUatId = reader.GetInt32(1);
                            bayForeign.SubstationWebUatId = reader.GetInt32(2);
                            bayForeign.BayTypeWebUatId = reader.GetInt32(3);
                            bayForeign.SourceEntityWebUatId = reader.GetInt32(4);
                            bayForeign.SourceEntityName = reader.GetString(5);
                            bayForeign.DestEntityWebUatId = reader.GetInt32(6);
                            bayForeign.DestEntityName = reader.GetString(7);
                            bayForeign.BayNumber = reader.GetString(8);
                            bayForeign.SourceEntityType = reader.GetString(9);
                            if (!reader.IsDBNull(10))
                            {
                                bayForeign.DestEntityType = reader.GetString(10);
                            }
                            bayForeign.Name = reader.GetString(11);
                            baysForeign.Add(bayForeign);
                        }
                        reader.Dispose();

                        return baysForeign;
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
