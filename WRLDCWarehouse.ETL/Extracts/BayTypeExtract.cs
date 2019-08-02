using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class BayTypeExtract
    {
        public List<BayType> ExtractBays(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, TYPE from REPORTING_WEB_UI_UAT.BAY_TYPE where :id=1";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<BayType> bayTypes = new List<BayType>();
                        while (reader.Read())
                        {
                            BayType bayType = new BayType();
                            bayType.WebUatId = reader.GetInt32(0);
                            bayType.Name = reader.GetString(1);
                            bayTypes.Add(bayType);
                        }

                        reader.Dispose();

                        return bayTypes;
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
