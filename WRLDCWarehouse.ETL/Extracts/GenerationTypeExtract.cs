using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class GenerationTypeExtract
    {
        public List<GenerationType> ExtractGenTypes(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select ID, NAME from GENERATING_STATION_TYPE where :id=1 and NAME IS NOT NULL and ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<GenerationType> genTypes = new List<GenerationType>();
                        while (reader.Read())
                        {
                            GenerationType genType = new GenerationType();
                            genType.WebUatId = reader.GetInt32(0);
                            genType.Name = reader.GetString(1);
                            genTypes.Add(genType);
                        }

                        reader.Dispose();

                        return genTypes;
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
