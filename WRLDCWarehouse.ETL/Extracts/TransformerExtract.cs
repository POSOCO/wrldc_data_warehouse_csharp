using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class TransformerExtract
    {
        public List<TransformerForeign> ExtractTransformersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, TRANSFORMER_NAME, HV_STATION_ID, CIRCUIT_NUMBER, TYPE_GT_ICT, MVA_CAPACITY, 
                                            DATETIME_OF_COMMISSIONING, DATETIME_OF_DECOMMISSIONING, DATETIME_OF_COD, LOCATION_ID, 
                                            STATIONTYPE, HV_VOLTAGE_LEVEL, LV_VOLTAGE_LEVEL from TRANSFORMER where :id=1 AND TRANSFORMER_NAME IS NOT NULL and STATIONTYPE IS NOT NULL 
                                            and HV_STATION_ID IS NOT NULL and CIRCUIT_NUMBER IS NOT NULL and TYPE_GT_ICT IS NOT NULL and LOCATION_ID IS NOT NULL AND HV_VOLTAGE_LEVEL IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<TransformerForeign> transformersForeign = new List<TransformerForeign>();
                        while (reader.Read())
                        {
                            TransformerForeign transForeign = new TransformerForeign();
                            transForeign.WebUatId = reader.GetInt32(0);
                            transForeign.Name = reader.GetString(1);
                            transForeign.HVStationWebUatId = reader.GetInt32(2);
                            transForeign.TransformerNumber = reader.GetInt32(3);
                            transForeign.TransTypeWebUatId = reader.GetInt32(4);
                            transForeign.MVACapacity = reader.GetDecimal(5);
                            transForeign.CommDate = reader.GetDateTime(6);
                            transForeign.DecommDate = reader.GetDateTime(7);
                            transForeign.CodDate = reader.GetDateTime(8);
                            transForeign.StateWebUatId = reader.GetInt32(9);
                            transForeign.StationType = reader.GetString(10);
                            transForeign.HighVoltLevelWebUatId = reader.GetInt32(11);
                            // checking for null values due to vendor non compliance
                            if (!reader.IsDBNull(12))
                            {
                                transForeign.LowVoltLevelWebUatId = reader.GetInt32(12);
                            }
                            else
                            {
                                transForeign.LowVoltLevelWebUatId = -1;
                            }
                            transformersForeign.Add(transForeign);
                        }

                        reader.Dispose();

                        return transformersForeign;
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
