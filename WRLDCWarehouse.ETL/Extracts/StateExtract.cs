using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class StateExtract
    {
        public List<StateForeign> ExtractStatesForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, SHORT_NAME, FULL_NAME, REGION_ID from SRLDC_STATE_MASTER where :id=1 and SHORT_NAME IS NOT NULL and ID IS NOT NULL and FULL_NAME IS NOT NULL and REGION_ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<StateForeign> states = new List<StateForeign>();
                        while (reader.Read())
                        {
                            StateForeign state = new StateForeign();
                            state.WebUatId = reader.GetInt32(0);
                            state.ShortName = reader.GetString(1);
                            state.FullName = reader.GetString(2);
                            state.RegionWebUatId = reader.GetInt32(3);
                            states.Add(state);
                        }

                        reader.Dispose();

                        return states;
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
