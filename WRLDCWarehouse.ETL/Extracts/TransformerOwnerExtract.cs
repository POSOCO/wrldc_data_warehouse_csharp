using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class TransformerOwnerExtract
    {
        public List<TransformerOwnerForeign> ExtractTransformerOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from ENTITY_ENTITY_RELN WHERE :id=1 and PARENT_ENTITY='TRANSFORMER' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<TransformerOwnerForeign> transOwnersForeign = new List<TransformerOwnerForeign>();
                        while (reader.Read())
                        {
                            TransformerOwnerForeign transOwnerForeign = new TransformerOwnerForeign();
                            transOwnerForeign.WebUatId = reader.GetInt32(0);
                            transOwnerForeign.TransformerWebUatId = reader.GetInt32(1);
                            transOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            transOwnersForeign.Add(transOwnerForeign);
                        }

                        reader.Dispose();

                        return transOwnersForeign;
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
