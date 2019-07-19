using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class OwnerExtract
    {
        public List<Owner> ExtractOwners(string oracleConnString)
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
                        cmd.CommandText = "select ID, OWNER_NAME from OWNER where :id=1 and OWNER_NAME IS NOT NULL and ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<Owner> owners = new List<Owner>();
                        while (reader.Read())
                        {
                            Owner owner = new Owner();
                            owner.Name = reader.GetString(1);
                            owner.WebUatId = reader.GetInt32(0);
                            owners.Add(owner);
                        }

                        reader.Dispose();

                        return owners;
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
