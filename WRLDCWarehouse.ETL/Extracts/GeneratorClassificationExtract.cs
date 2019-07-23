using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class GeneratorClassificationExtract
    {
        public List<GeneratorClassification> ExtractGeneratorClassifications(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, CLASSIFICATION from CLASSIFICATION_MASTER";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        // Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<GeneratorClassification> genClassfications = new List<GeneratorClassification>();
                        while (reader.Read())
                        {
                            GeneratorClassification genClassification = new GeneratorClassification();
                            genClassification.WebUatId = reader.GetInt32(0);
                            genClassification.Name = reader.GetString(1);
                            genClassfications.Add(genClassification);
                        }

                        reader.Dispose();

                        return genClassfications;
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
