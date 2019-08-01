using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class LineReactorOwnerExtract
    {
        public List<LineReactorOwnerForeign> ExtractLineReactorOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"SELECT ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from 
                                            REPORTING_WEB_UI_UAT.ENTITY_ENTITY_RELN WHERE :id=1 and PARENT_ENTITY='LINE_REACTOR' 
                                            and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<LineReactorOwnerForeign> lrOwnersForeign = new List<LineReactorOwnerForeign>();
                        while (reader.Read())
                        {
                            LineReactorOwnerForeign lrOwnerForeign = new LineReactorOwnerForeign();
                            lrOwnerForeign.WebUatId = reader.GetInt32(0);
                            lrOwnerForeign.LineReactorWebUatId = reader.GetInt32(1);
                            lrOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            lrOwnersForeign.Add(lrOwnerForeign);
                        }

                        reader.Dispose();

                        return lrOwnersForeign;
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
