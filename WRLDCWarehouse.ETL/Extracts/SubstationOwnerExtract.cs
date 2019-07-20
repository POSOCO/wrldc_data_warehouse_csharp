using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class SubstationOwnerExtract
    {
        public List<SubstationOwnerForeign> ExtractSubstationOwnersForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from ENTITY_ENTITY_RELN where :id=1 and PARENT_ENTITY='ASSOCIATE_SUBSTATION' and CHILD_ENTITY='OWNER' and PARENT_ENTITY_ATTRIBUTE='Owner' and CHILD_ENTITY_ATTRIBUTE='OwnerId'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<SubstationOwnerForeign> regions = new List<SubstationOwnerForeign>();
                        while (reader.Read())
                        {
                            SubstationOwnerForeign ssOwnerForeign = new SubstationOwnerForeign();
                            ssOwnerForeign.WebUatId = reader.GetInt32(0);
                            ssOwnerForeign.SubstationWebUatId = reader.GetInt32(1);
                            ssOwnerForeign.OwnerWebUatId = reader.GetInt32(2);
                            regions.Add(ssOwnerForeign);
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
