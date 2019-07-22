using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class AcTransLineCircuitCondTypeExtract
    {
        public List<AcTransLineCktCondTypeForeign> ExtractAcTransLineCktCondTypeForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID 
                                            from ENTITY_ENTITY_RELN 
                                            where :id=1 and ID IS NOT NULL and PARENT_ENTITY='AC_TRANSMISSION_LINE' and PARENT_ENTITY_ATTRIBUTE='ConductorType' 
                                            and CHILD_ENTITY='CONDUCTOR_MASTER' and CHILD_ENTITY_ATTRIBUTE='Id'";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<AcTransLineCktCondTypeForeign> acLineCondTypesForeign = new List<AcTransLineCktCondTypeForeign>();
                        while (reader.Read())
                        {
                            AcTransLineCktCondTypeForeign acLineCondTypeForeign = new AcTransLineCktCondTypeForeign();
                            acLineCondTypeForeign.WebUatId = reader.GetInt32(0);
                            acLineCondTypeForeign.AcTransLineCktWebUatId = reader.GetInt32(1);
                            acLineCondTypeForeign.CondTypeWebUatId = reader.GetInt32(2);
                            acLineCondTypesForeign.Add(acLineCondTypeForeign);
                        }

                        reader.Dispose();

                        return acLineCondTypesForeign;
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
