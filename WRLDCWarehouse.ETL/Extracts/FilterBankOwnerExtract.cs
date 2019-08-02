using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class FilterBankOwnerExtract
    {
        public List<FilterBankOwnerForeign> ExtractFilterBankOwnersForeign(string oracleConnString)
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
                                            REPORTING_WEB_UI_UAT.ENTITY_ENTITY_RELN WHERE :id=1 and PARENT_ENTITY='FILTER_BANK' 
                                            and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<FilterBankOwnerForeign> fbOwnersForeign = new List<FilterBankOwnerForeign>();
                        while (reader.Read())
                        {
                            FilterBankOwnerForeign fbOwnerForeign = new FilterBankOwnerForeign();
                            fbOwnerForeign.WebUatId = reader.GetInt32(0);
                            fbOwnerForeign.FilterBankWebUatId = reader.GetInt32(1);
                            fbOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            fbOwnersForeign.Add(fbOwnerForeign);
                        }

                        reader.Dispose();

                        return fbOwnersForeign;
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
