using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class ConductorTypeExtract
    {
        public List<ConductorType> ExtractConductorTypes(string oracleConnString)
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
                        cmd.CommandText = "select ID, CONDUCTOR_TYPE from CONDUCTOR_MASTER where :id=1 and CONDUCTOR_TYPE IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<ConductorType> condTypes = new List<ConductorType>();
                        while (reader.Read())
                        {
                            ConductorType condType = new ConductorType();
                            condType.WebUatId = reader.GetInt32(0);
                            condType.Name = reader.GetString(1);
                            condTypes.Add(condType);
                        }

                        reader.Dispose();

                        return condTypes;
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
