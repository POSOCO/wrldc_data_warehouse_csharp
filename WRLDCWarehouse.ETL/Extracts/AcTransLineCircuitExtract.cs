using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class AcTransLineCircuitExtract
    {
        public List<AcTransmissionLineCircuitForeign> ExtractAcTransLineCktForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, LINE_CIRCUIT_NAME, LINE_ID, CIRCUIT_NUMBER, 
                                            LENGTH, FIRST_CHARGING_DATETIME, TRAIL_OPERATION_DATETIME, COMMISSIONING_DATETIME, 
                                            COD_DATETIME, DECOMMISSIONING_DATETIME, THERMAL_LIMIT_MVA, SIL_MW 
                                            from AC_TRANSMISSION_LINE_CIRCUIT 
                                            where :id=1 and LINE_ID IS NOT NULL and ID IS NOT NULL and VOLTAGE_LEVEL IS NOT NULL and CIRCUIT_NUMBER IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<AcTransmissionLineCircuitForeign> acTranLineCktsForeign = new List<AcTransmissionLineCircuitForeign>();
                        while (reader.Read())
                        {
                            AcTransmissionLineCircuitForeign acTransLineCktForeign = new AcTransmissionLineCircuitForeign();
                            acTransLineCktForeign.WebUatId = reader.GetInt32(0);
                            acTransLineCktForeign.Name = reader.GetString(1);
                            acTransLineCktForeign.AcTransLineWebUatId = reader.GetInt32(2);
                            acTransLineCktForeign.CktNumber = reader.GetInt32(3);
                            acTransLineCktForeign.Length = reader.GetDecimal(4);
                            acTransLineCktForeign.FtcDate = reader.GetDateTime(5);
                            acTransLineCktForeign.TrialOperationDate = reader.GetDateTime(6);
                            acTransLineCktForeign.CommDate = reader.GetDateTime(7);
                            acTransLineCktForeign.CODDate = reader.GetDateTime(8);
                            acTransLineCktForeign.DeCommDate = reader.GetDateTime(9);
                            acTransLineCktForeign.ThermalLimitMVA = reader.GetDecimal(10);
                            acTransLineCktForeign.SIL = reader.GetDecimal(11);
                            acTranLineCktsForeign.Add(acTransLineCktForeign);
                        }

                        reader.Dispose();

                        return acTranLineCktsForeign;
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
