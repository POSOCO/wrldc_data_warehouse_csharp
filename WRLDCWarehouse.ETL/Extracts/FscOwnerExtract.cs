using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class FscOwnerExtract
    {
        public List<FscOwnerForeign> ExtractFscOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"SELECT ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from REPORTING_WEB_UI_UAT.ENTITY_ENTITY_RELN 
                                            WHERE PARENT_ENTITY='FSC' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<FscOwnerForeign> fscOwnersForeign = new List<FscOwnerForeign>();
                        while (reader.Read())
                        {
                            FscOwnerForeign fscOwnerForeign = new FscOwnerForeign();
                            fscOwnerForeign.WebUatId = reader.GetInt32(0);
                            fscOwnerForeign.FscWebUatId = reader.GetInt32(1);
                            fscOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            fscOwnersForeign.Add(fscOwnerForeign);
                        }

                        reader.Dispose();

                        return fscOwnersForeign;
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
