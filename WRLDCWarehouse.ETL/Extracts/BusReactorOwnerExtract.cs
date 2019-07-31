using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class BusReactorOwnerExtract
    {
        public List<BusReactorOwnerForeign> ExtractBusReactorOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"SELECT ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from ENTITY_ENTITY_RELN WHERE :id=1 
                                            and PARENT_ENTITY='BUS_REACTOR' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY='OWNER' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<BusReactorOwnerForeign> brOwnersForeign = new List<BusReactorOwnerForeign>();
                        while (reader.Read())
                        {
                            BusReactorOwnerForeign brOwnerForeign = new BusReactorOwnerForeign();
                            brOwnerForeign.WebUatId = reader.GetInt32(0);
                            brOwnerForeign.BusReactorWebUatId = reader.GetInt32(1);
                            brOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            brOwnersForeign.Add(brOwnerForeign);
                        }

                        reader.Dispose();

                        return brOwnersForeign;
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
