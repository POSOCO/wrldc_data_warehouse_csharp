using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class BayOwnerExtract
    {
        public List<BayOwnerForeign> ExtractBayOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"SELECT ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from ENTITY_ENTITY_RELN WHERE :id=1 
                                            and PARENT_ENTITY='BAY' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<BayOwnerForeign> bayOwnersForeign = new List<BayOwnerForeign>();
                        while (reader.Read())
                        {
                            BayOwnerForeign bayOwnerForeign = new BayOwnerForeign();
                            bayOwnerForeign.WebUatId = reader.GetInt32(0);
                            bayOwnerForeign.BayWebUatId = reader.GetInt32(1);
                            bayOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            bayOwnersForeign.Add(bayOwnerForeign);
                        }

                        reader.Dispose();

                        return bayOwnersForeign;
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
