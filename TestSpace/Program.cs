using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;

namespace TestSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test - Extarcting Voltage Levels");
            string oracleConnString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={Environment.GetEnvironmentVariable("REPORTING_DB_HOST")})(PORT={Environment.GetEnvironmentVariable("REPORTING_DB_PORT")})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={Environment.GetEnvironmentVariable("REPORTING_DB_SERVICENAME")})));User Id={Environment.GetEnvironmentVariable("REPORTING_DB_WEBUAT_USERNAME")};Password={Environment.GetEnvironmentVariable("REPORTING_DB_WEBUAT_PASSWORD")};";
            VoltLevelExtract voltLevelExtract = new VoltLevelExtract();
            List<VoltLevel> volts = voltLevelExtract.ExtractVoltageLevels(oracleConnString);
            Console.WriteLine(volts);
        }

        public void ConnTest()
        {
            Console.WriteLine("Oracle Data fetch test!");
            //Create a connection to Oracle			
            string conString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={Environment.GetEnvironmentVariable("REPORTING_DB_HOST")})(PORT={Environment.GetEnvironmentVariable("REPORTING_DB_PORT")})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={Environment.GetEnvironmentVariable("REPORTING_DB_SERVICENAME")})));User Id={Environment.GetEnvironmentVariable("REPORTING_DB_USERNAME")};Password={Environment.GetEnvironmentVariable("REPORTING_DB_PASSWORD")};";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select NAME from DIM_MAJOR_SUBSTATION where :id=1";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Major Substation Name: " + reader.GetString(0) + "\n");
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
