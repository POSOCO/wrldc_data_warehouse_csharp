using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class BusExtract
    {
        public List<BusForeign> ExtractBusesForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, BUS_NAME, BUS_NUMBER, VOLTAGE, FK_SUBSTATION_ID from BUS where 1=1 and BUS_NAME IS NOT NULL and ID IS NOT NULL and BUS_NUMBER IS NOT NULL and VOLTAGE IS NOT NULL and FK_SUBSTATION_ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<BusForeign> busesForeign = new List<BusForeign>();
                        while (reader.Read())
                        {
                            BusForeign busForeign = new BusForeign();
                            busForeign.WebUatId = reader.GetInt32(0);
                            busForeign.Name = reader.GetString(1);
                            busForeign.BusNumber = reader.GetInt32(2);
                            busForeign.VoltageWebUatId = reader.GetInt32(3);
                            busForeign.AssSubstationWebUatId = reader.GetInt32(4);
                            busesForeign.Add(busForeign);
                        }

                        reader.Dispose();

                        return busesForeign;
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
