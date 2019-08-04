using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class CompensatorExtract
    {
        public List<CompensatorForeign> ExtractCompensatorForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, TCSC_NAME, TCSC_TYPE_ID, TCSC_NUMBER, FK_SUBSTATION, LINE_TYPE_ID, 
                                            FK_LINE, LOCATION_ID, PERC_VARIABLE_COMPENSATION, PERC_FIXED_COMPENSATION, LINE_REACTANCE_OHMS, 
                                            DATETIME_OF_COMMISSIONING, DATETIME_OF_COD, DATETIME_OF_DECOMMISSIONING from REPORTING_WEB_UI_UAT.TCSC where :id=1";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<CompensatorForeign> compensatorsForeign = new List<CompensatorForeign>();
                        while (reader.Read())
                        {
                            CompensatorForeign compensatorForeign = new CompensatorForeign();
                            compensatorForeign.WebUatId = reader.GetInt32(0);
                            compensatorForeign.Name = reader.GetString(1);
                            compensatorForeign.CompensatorTypeWebUatId = reader.GetInt32(2);
                            compensatorForeign.CompensatorNumber = reader.GetInt32(3);
                            compensatorForeign.SubstationWebUatId = reader.GetInt32(4);
                            compensatorForeign.AttachElementType = reader.GetInt32(5);
                            compensatorForeign.AttachElementWebUatId = reader.GetInt32(6);
                            compensatorForeign.StateWebUatId = reader.GetInt32(7);
                            compensatorForeign.PercVariableComp = reader.GetInt32(8);
                            compensatorForeign.PercFixedComp = reader.GetInt32(9);
                            compensatorForeign.LineReactanceOhms = reader.GetInt32(10);
                            compensatorForeign.CommDate = reader.GetDateTime(11);
                            compensatorForeign.CodDate = reader.GetDateTime(12);
                            compensatorForeign.DeCommDate = reader.GetDateTime(13);
                            compensatorsForeign.Add(compensatorForeign);
                        }

                        reader.Dispose();

                        return compensatorsForeign;
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
