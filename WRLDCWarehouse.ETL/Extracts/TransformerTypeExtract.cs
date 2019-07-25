using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class TransformerTypeExtract
    {
        public List<TransformerType> ExtractTransformerTypes(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT TRANS_ELEMENT_TYPE_ID, TRANS_ELEMENT_TYPE FROM TRANS_ELEMENT_TYPE_MASTER where ELEMENT_CATEGORY = 'Transformer' and :id=1 and TRANS_ELEMENT_TYPE IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<TransformerType> transTypes = new List<TransformerType>();
                        while (reader.Read())
                        {
                            TransformerType transType = new TransformerType();
                            transType.WebUatId = reader.GetInt32(0);
                            transType.Name = reader.GetString(1);
                            transTypes.Add(transType);
                        }

                        reader.Dispose();

                        return transTypes;
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
