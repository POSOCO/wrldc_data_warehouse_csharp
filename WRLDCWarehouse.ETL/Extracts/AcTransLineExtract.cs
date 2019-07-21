using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class AcTransLineExtract
    {
        public List<AcTransmissionLineForeign> ExtractAcTransLineForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, LINE_NAME, VOLTAGE_LEVEL, FROM_SUB_ID, TO_SUB_ID from AC_TRANS_LINE_MASTER where :id=1 and LINE_NAME IS NOT NULL and ID IS NOT NULL and VOLTAGE_LEVEL IS NOT NULL and FROM_SUB_ID IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<AcTransmissionLineForeign> acTranLinesForeign = new List<AcTransmissionLineForeign>();
                        while (reader.Read())
                        {
                            AcTransmissionLineForeign acTransLineForeign = new AcTransmissionLineForeign();
                            acTransLineForeign.WebUatId = reader.GetInt32(0);
                            acTransLineForeign.Name = reader.GetString(1);
                            acTransLineForeign.VoltLevelWebUatId = reader.GetInt32(2);
                            acTransLineForeign.FromSSWebUatId = reader.GetInt32(3);
                            acTransLineForeign.ToSSWebUatId = reader.GetInt32(3);
                            acTranLinesForeign.Add(acTransLineForeign);
                        }

                        reader.Dispose();

                        return acTranLinesForeign;
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
