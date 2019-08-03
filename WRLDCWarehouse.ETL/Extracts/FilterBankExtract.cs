using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class FilterBankExtract
    {
        public List<FilterBankForeign> ExtractFilterBanksForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, REGION_ID, STATE_ID, SUBSTATION_ID, VOLTAGE_ID, 
                                            MVAR, IS_SWITCHABLE, FILTERBANK_NUMBER, FILTERBANK_NAME from REPORTING_WEB_UI_UAT.FILTER_BANK where :id=1";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<FilterBankForeign> fbsForeign = new List<FilterBankForeign>();
                        while (reader.Read())
                        {
                            FilterBankForeign fbForeign = new FilterBankForeign();
                            fbForeign.WebUatId = reader.GetInt32(0);
                            fbForeign.RegionWebUatId = reader.GetInt32(1);
                            fbForeign.StateWebUatId = reader.GetInt32(2);
                            fbForeign.SubstationWebUatId = reader.GetInt32(3);
                            fbForeign.VoltLevelWebUatId = reader.GetInt32(4);
                            fbForeign.MVARCapacity = reader.GetInt32(5);
                            fbForeign.IsSwitchable = reader.GetInt32(6);
                            fbForeign.FilterBankNumber = reader.GetInt32(7);
                            if (!reader.IsDBNull(8))
                            {
                                fbForeign.Name = reader.GetString(8);
                            }
                            fbsForeign.Add(fbForeign);
                        }
                        reader.Dispose();

                        return fbsForeign;
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
