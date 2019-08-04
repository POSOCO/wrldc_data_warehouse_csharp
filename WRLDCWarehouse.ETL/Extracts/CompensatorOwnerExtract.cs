using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class CompensatorOwnerExtract
    {
        public List<CompensatorOwnerForeign> ExtractCompensatorOwnersForeign(string oracleConnString)
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
                                            WHERE PARENT_ENTITY='TCSC' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<CompensatorOwnerForeign> compensatorOwnersForeign = new List<CompensatorOwnerForeign>();
                        while (reader.Read())
                        {
                            CompensatorOwnerForeign compensatorOwnerForeign = new CompensatorOwnerForeign();
                            compensatorOwnerForeign.WebUatId = reader.GetInt32(0);
                            compensatorOwnerForeign.CompensatorWebUatId = reader.GetInt32(1);
                            compensatorOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            compensatorOwnersForeign.Add(compensatorOwnerForeign);
                        }

                        reader.Dispose();

                        return compensatorOwnersForeign;
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
