using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class LineReactorExtract
    {
        public List<LineReactorForeign> ExtractLineReactorsForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"SELECT ID, FK_LINE_ID, FK_SUBSTATION, MVAR_CAPACITY, DATETIME_OF_COMMISSIONING, DATETIME_OF_COD, 
                                            DATETIME_OF_DECOMMISSIONING, LOCATION_ID, REACTOR_NAME, ISCONVERTIBLE, ISSWITCHABLE 
                                            FROM REPORTING_WEB_UI_UAT.LINE_REACTOR WHERE :id=1 and LINE_TYPE_ID=2";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<LineReactorForeign> lrsForeign = new List<LineReactorForeign>();
                        while (reader.Read())
                        {
                            LineReactorForeign lrForeign = new LineReactorForeign();
                            lrForeign.WebUatId = reader.GetInt32(0);
                            lrForeign.AcTransLineCktWebUatId = reader.GetInt32(1);
                            lrForeign.SubstationWebUatId = reader.GetInt32(2);
                            lrForeign.MvarCapacity = reader.GetDecimal(3);
                            lrForeign.CommDate = reader.GetDateTime(4);
                            lrForeign.CodDate = reader.GetDateTime(5);
                            lrForeign.DecommDate = reader.GetDateTime(6);
                            lrForeign.StateWebUatId = reader.GetInt32(7);
                            lrForeign.Name = reader.GetString(8);
                            lrForeign.IsConvertible = reader.GetInt32(9);
                            lrForeign.IsSwitchable = reader.GetInt32(10);
                            lrsForeign.Add(lrForeign);
                        }
                        reader.Dispose();

                        return lrsForeign;
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
