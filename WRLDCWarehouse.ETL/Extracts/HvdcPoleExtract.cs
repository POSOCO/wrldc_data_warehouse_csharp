using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class HvdcPoleExtract
    {
        public List<HvdcPoleForeign> ExtractHvdcPoleForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, POLE_NAME, POLE_ID, FK_SUBSTATION, LOCATION_ID, POLE_TYPE, 
                                            VOLTAGE_LEVEL, MAX_FIRING_ANGLE_DEG, MIN_FIRING_ANGLE_DEG, DATETIME_OF_COMMISSIONING, 
                                            DATETIME_OF_COD, DATETIME_OF_DECOMMISSIONING from REPORTING_WEB_UI_UAT.HVDC_POLE 
                                            where :id=1 and POLE_NAME IS NOT NULL and ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<HvdcPoleForeign> hvdcPolesForeign = new List<HvdcPoleForeign>();
                        while (reader.Read())
                        {
                            HvdcPoleForeign hvdcPoleForeign = new HvdcPoleForeign();
                            hvdcPoleForeign.WebUatId = reader.GetInt32(0);
                            hvdcPoleForeign.Name = reader.GetString(1);
                            hvdcPoleForeign.PoleNumber = reader.GetInt32(2);
                            hvdcPoleForeign.SubstationWebUatId = reader.GetInt32(3);
                            hvdcPoleForeign.StateWebUatId = reader.GetInt32(4);
                            hvdcPoleForeign.PoleType = reader.GetString(5);
                            hvdcPoleForeign.VoltLevelWebUatId = reader.GetInt32(6);
                            hvdcPoleForeign.MaxFiringAngleDegrees = reader.GetDecimal(7);
                            hvdcPoleForeign.MinFiringAngleDegrees = reader.GetDecimal(8);
                            hvdcPoleForeign.CommDate = reader.GetDateTime(9);
                            hvdcPoleForeign.CodDate = reader.GetDateTime(10);
                            hvdcPoleForeign.DeCommDate = reader.GetDateTime(11);
                            hvdcPolesForeign.Add(hvdcPoleForeign);
                        }

                        reader.Dispose();

                        return hvdcPolesForeign;
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
