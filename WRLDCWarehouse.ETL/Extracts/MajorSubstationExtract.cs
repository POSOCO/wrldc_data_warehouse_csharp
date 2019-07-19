using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class MajorSubstationExtract
    {
        public List<MajorSubstationForeign> ExtractMajorSubstationsForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, NAME, STATE_ID from MAJOR_SUBSTATION where :id=1 and NAME IS NOT NULL and ID IS NOT NULL and STATE_ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<MajorSubstationForeign> majorSubstationsForeign = new List<MajorSubstationForeign>();
                        while (reader.Read())
                        {
                            MajorSubstationForeign majorSSForeign = new MajorSubstationForeign();
                            majorSSForeign.WebUatId = reader.GetInt32(0);
                            majorSSForeign.Name = reader.GetString(1);
                            majorSSForeign.StateWebUatId = reader.GetInt32(2);
                            majorSubstationsForeign.Add(majorSSForeign);
                        }

                        reader.Dispose();

                        return majorSubstationsForeign;
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
