using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class FscExtract
    {
        public List<FscForeign> ExtractFscForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, FSC_NAME, FK_LINE, FK_SUBSTATION, LOCATION_ID, PERC_COMPENSATION, 
                                            LINE_REACTANCE_OHMS, CAPACITOR_REACTANCE_OHMS, RATED_MVAR_PHASE, RATED_CURRENT_AMPS, 
                                            DATETIME_OF_COMMISSIONING, DATETIME_OF_COD, DATETIME_OF_DECOMMISSIONING from REPORTING_WEB_UI_UAT.FSC where :id=1";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<FscForeign> fscsForeign = new List<FscForeign>();
                        while (reader.Read())
                        {
                            FscForeign fscForeign = new FscForeign();
                            fscForeign.WebUatId = reader.GetInt32(0);
                            fscForeign.Name = reader.GetString(1);
                            fscForeign.AcTransLineCktWebUatId = reader.GetInt32(2);
                            fscForeign.SubstationWebUatId = reader.GetInt32(3);
                            fscForeign.StateWebUatId = reader.GetInt32(4);
                            fscForeign.PercComp = reader.GetInt32(5);
                            fscForeign.LineReactance = reader.GetInt32(6);
                            fscForeign.CapacitiveReactance = reader.GetInt32(7);
                            fscForeign.RatedMvarPhase = reader.GetInt32(8);
                            fscForeign.RatedCurrentAmps = reader.GetInt32(9);
                            fscForeign.CommDate = reader.GetDateTime(10);
                            fscForeign.CodDate = reader.GetDateTime(11);
                            fscForeign.DeCommDate = reader.GetDateTime(12);
                            fscsForeign.Add(fscForeign);
                        }

                        reader.Dispose();

                        return fscsForeign;
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
