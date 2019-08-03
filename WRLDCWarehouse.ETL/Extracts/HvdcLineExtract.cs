using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class HvdcLineExtract
    {
        public List<HvdcLineForeign> ExtractHvdcLineForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID,LINE_NAME,FROM_SUB_ID,TO_SUB_ID,FROM_VOLTAGE,FROM_LOCATION_ID,TO_LOCATION_ID from REPORTING_WEB_UI_UAT.HVDC_LINE_MASTER 
                                            where :id=1 and LINE_NAME IS NOT NULL and ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<HvdcLineForeign> hvdcLinesForeign = new List<HvdcLineForeign>();
                        while (reader.Read())
                        {
                            HvdcLineForeign hvdcLineForeign = new HvdcLineForeign();
                            hvdcLineForeign.WebUatId = reader.GetInt32(0);
                            hvdcLineForeign.Name = reader.GetString(1);
                            hvdcLineForeign.FromSSWebUatId = reader.GetInt32(2);
                            hvdcLineForeign.ToSSWebUatId = reader.GetInt32(3);
                            hvdcLineForeign.VoltLevelWebUatId = reader.GetInt32(4);
                            hvdcLineForeign.FromStateWebUatId = reader.GetInt32(5);
                            hvdcLineForeign.ToStateWebUatId = reader.GetInt32(6);
                            hvdcLinesForeign.Add(hvdcLineForeign);
                        }

                        reader.Dispose();

                        return hvdcLinesForeign;
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
