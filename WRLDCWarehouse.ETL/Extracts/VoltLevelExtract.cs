using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class VoltLevelExtract
    {
        public List<VoltLevel> ExtractVoltageLevels(string oracleConnStr)
        {
            //Create a connection to Oracle			
            string conString = oracleConnStr;

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
                        cmd.CommandText = "select TRANS_ELEMENT_TYPE_ID, TRANS_ELEMENT_TYPE, ELEMENT_CATEGORY from TRANS_ELEMENT_TYPE_MASTER where :id=1 and TRANS_ELEMENT_TYPE_ID IS NOT NULL and TRANS_ELEMENT_TYPE IS NOT NULL and ELEMENT_CATEGORY IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<VoltLevel> volts = new List<VoltLevel>();
                        while (reader.Read())
                        {
                            VoltLevel volt = new VoltLevel();
                            volt.Name = reader.GetString(1);
                            volt.WebUatId = reader.GetInt32(0);
                            volt.EntityType = reader.GetString(2);
                            volts.Add(volt);
                        }

                        reader.Dispose();

                        return volts;
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
