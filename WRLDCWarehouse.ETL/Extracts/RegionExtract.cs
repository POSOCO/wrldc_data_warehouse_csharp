using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.Entities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class RegionExtract
    {
        public List<Region> ExtractRegions(string oracleConnString)
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
                        cmd.CommandText = "select ID, FULL_NAME, SHORT_NAME from REGION_MASTER where :id=1 and FULL_NAME IS NOT NULL and ID IS NOT NULL and SHORT_NAME IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<Region> regions = new List<Region>();
                        while (reader.Read())
                        {
                            Region region = new Region();
                            region.WebUatId = reader.GetInt32(0);
                            region.FullName = reader.GetString(1);
                            region.ShortName = reader.GetString(2);
                            regions.Add(region);
                        }

                        reader.Dispose();

                        return regions;
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
