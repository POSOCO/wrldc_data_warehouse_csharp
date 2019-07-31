using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class BusReactorExtract
    {
        public List<BusReactorForeign> ExtractBusReactorsForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, REACTOR_NO, FK_BUS, FK_SUBSTATION, MVAR_CAPACITY, DATETIME_OF_COMMISSIONING, DATETIME_OF_COD, 
                                            DATETIME_OF_DECOMMISSIONING, LOCATION_ID, REACTOR_NAME from REPORTING_WEB_UI_UAT.BUS_REACTOR where :id=1";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<BusReactorForeign> brsForeign = new List<BusReactorForeign>();
                        while (reader.Read())
                        {
                            BusReactorForeign brForeign = new BusReactorForeign();
                            brForeign.WebUatId = reader.GetInt32(0);
                            brForeign.BusReactorNumber = reader.GetInt32(1);
                            brForeign.BusWebUatId = reader.GetInt32(2);
                            brForeign.SubstationWebUatId = reader.GetInt32(3);
                            brForeign.MvarCapacity = reader.GetDecimal(4);
                            brForeign.CommDate = reader.GetDateTime(5);
                            brForeign.CodDate = reader.GetDateTime(6);
                            brForeign.DecommDate = reader.GetDateTime(7);
                            brForeign.StateWebUatId = reader.GetInt32(8);
                            brForeign.Name = reader.GetString(9);
                            brsForeign.Add(brForeign);
                        }
                        reader.Dispose();

                        return brsForeign;
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
