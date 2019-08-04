using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class CompensatorTypeExtract
    {
        public List<CompensatorType> ExtractCompensatorTypes(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, TYPE from REPORTING_WEB_UI_UAT.TCSC_TYPE where :id=1 and ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<CompensatorType> compensatorTypes = new List<CompensatorType>();
                        while (reader.Read())
                        {
                            CompensatorType compensatorType = new CompensatorType();
                            compensatorType.WebUatId = reader.GetInt32(0);
                            compensatorType.Name = reader.GetString(1);
                            compensatorTypes.Add(compensatorType);
                        }

                        reader.Dispose();

                        return compensatorTypes;
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
