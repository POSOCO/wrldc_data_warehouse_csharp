using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class SubstationExtract
    {
        public List<SubstationForeign> ExtractSubstationsForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, SUBSTATION_NAME, SUB_ID, VOLTAGE_LEVEL, TYPE_AC_DC, LOCATION_ID, DATE_OF_COMMISSIONING, DATE_OF_DECOMMISSIONING, DATE_OF_COD from ASSOCIATE_SUBSTATION where :id=1 and SUB_ID IS NOT NULL and ID IS NOT NULL and VOLTAGE_LEVEL IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<SubstationForeign> substationsForeign = new List<SubstationForeign>();
                        while (reader.Read())
                        {
                            SubstationForeign ssForeign = new SubstationForeign();
                            ssForeign.WebUatId = reader.GetInt32(0);
                            ssForeign.Name = reader.GetString(1);
                            ssForeign.MajorSubstationWebUatId = reader.GetInt32(2);
                            ssForeign.VoltLevelWebUatId = reader.GetDouble(3);
                            ssForeign.Classification = reader.GetString(4);
                            ssForeign.StateWebUatId = reader.GetString(5);
                            ssForeign.CommDate = reader.GetDateTime(6);
                            ssForeign.DecommDate = reader.GetDateTime(7);
                            ssForeign.CodDate = reader.GetDateTime(8);
                            substationsForeign.Add(ssForeign);
                        }

                        reader.Dispose();

                        return substationsForeign;
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
