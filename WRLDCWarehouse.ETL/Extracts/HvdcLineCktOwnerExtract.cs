using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class HvdcLineCktOwnerExtract
    {
        public List<HvdcLineCktOwnerForeign> ExtractHvdcLineCktOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"SELECT ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from REPORTING_WEB_UI_UAT.ENTITY_ENTITY_RELN 
                                            WHERE PARENT_ENTITY='HVDC_LINE' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<HvdcLineCktOwnerForeign> cktOwnersForeign = new List<HvdcLineCktOwnerForeign>();
                        while (reader.Read())
                        {
                            HvdcLineCktOwnerForeign cktOwnerForeign = new HvdcLineCktOwnerForeign();
                            cktOwnerForeign.WebUatId = reader.GetInt32(0);
                            cktOwnerForeign.HvdcLineCktWebUatId = reader.GetInt32(1);
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
