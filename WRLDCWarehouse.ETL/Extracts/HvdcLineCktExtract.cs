using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class HvdcLineCktExtract
    {
        public List<HvdcLineCktForeign> ExtractHvdcLineCktForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, LINE_CIRCUIT_NAME, HVDC_LINE_ID, CIRCUIT_NO, FROM_SUBSTATION_ID, TO_SUBSTATION_ID,
                                            FROM_BUS_ID, TO_BUS_ID, LENGTH_KM, DATETIME_OF_TRAIL_OPERATION, DATETIME_OF_FIRST_CHARGING, 
                                            DATETIME_OF_COMMISSIONING, DATETIME_OF_COD, DATETIME_OF_DECOMMISSIONING, 
                                            THERMAL_LIMIT_MVA, NO_OF_CONDUCTORS_PER_CKT from REPORTING_WEB_UI_UAT.HVDC_LINE_CIRCUIT 
                                            where :id=1 and HVDC_LINE_ID IS NOT NULL and ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<HvdcLineCktForeign> hvdcLineCktsForeign = new List<HvdcLineCktForeign>();
                        while (reader.Read())
                        {
                            HvdcLineCktForeign hvdcLineCktForeign = new HvdcLineCktForeign();
                            hvdcLineCktForeign.WebUatId = reader.GetInt32(0);
                            hvdcLineCktForeign.Name = reader.GetString(1);
                            hvdcLineCktForeign.hvdcLineWebUatId = reader.GetInt32(2);
                            hvdcLineCktForeign.CktNumber = reader.GetInt32(3);
                            hvdcLineCktForeign.FromSubstationWebUatId = reader.GetInt32(4);
                            hvdcLineCktForeign.ToSubstationWebUatId = reader.GetInt32(5);
                            hvdcLineCktForeign.FromBusWebUatId = reader.GetInt32(6);
                            hvdcLineCktForeign.ToBusWebUatId = reader.GetInt32(7);
                            hvdcLineCktForeign.Length = reader.GetDecimal(8);
                            hvdcLineCktForeign.TrialOperationDate = reader.GetDateTime(9);
                            hvdcLineCktForeign.FtcDate = reader.GetDateTime(10);
                            hvdcLineCktForeign.CommDate = reader.GetDateTime(11);
                            hvdcLineCktForeign.CODDate = reader.GetDateTime(12);
                            hvdcLineCktForeign.DeCommDate = reader.GetDateTime(13);
                            hvdcLineCktForeign.ThermalLimitMVA = reader.GetDecimal(14);
                            hvdcLineCktForeign.NumConductorsPerCkt = reader.GetInt32(15);
                            hvdcLineCktsForeign.Add(hvdcLineCktForeign);
                        }

                        reader.Dispose();

                        return hvdcLineCktsForeign;
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
