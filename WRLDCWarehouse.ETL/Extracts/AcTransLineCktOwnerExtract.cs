using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class AcTransLineCktOwnerExtract
    {
        public List<AcTransmissionLineCircuitOwnerForeign> ExtractAcTransLineCktOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from ENTITY_ENTITY_RELN where :id=1 and PARENT_ENTITY='AC_TRANSMISSION_LINE' and CHILD_ENTITY='OWNER' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<AcTransmissionLineCircuitOwnerForeign> cktOwnersForeign = new List<AcTransmissionLineCircuitOwnerForeign>();
                        while (reader.Read())
                        {
                            AcTransmissionLineCircuitOwnerForeign cktOwnerForeign = new AcTransmissionLineCircuitOwnerForeign();
                            cktOwnerForeign.WebUatId = reader.GetInt32(0);
                            cktOwnerForeign.AcTranLineCktWebUatId = reader.GetInt32(1);
                            cktOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            cktOwnersForeign.Add(cktOwnerForeign);
                        }

                        reader.Dispose();

                        return cktOwnersForeign;
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
