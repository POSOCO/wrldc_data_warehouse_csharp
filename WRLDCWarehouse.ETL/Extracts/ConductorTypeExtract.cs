using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class ConductorTypeExtract
    {
        public List<ConductorType> ExtractConductorTypes(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select ID, CONDUCTOR_TYPE from CONDUCTOR_MASTER where :id=1 and CONDUCTOR_TYPE IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<ConductorType> condTypes = new List<ConductorType>();
                        while (reader.Read())
                        {
                            ConductorType condType = new ConductorType();
                            condType.WebUatId = reader.GetInt32(0);
                            condType.Name = reader.GetString(1);
                            condTypes.Add(condType);
                        }

                        reader.Dispose();

                        return condTypes;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }
        }

        public List<AcTransLineCktCondTypeForeign> ExtractLineConductorTypesForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select ID, PARENT_ENTITY_ATTRIBUTE_ID, CHILD_ENTITY_ATTRIBUTE_ID from ENTITY_ENTITY_RELN where :id=1 and PARENT_ENTITY='AC_TRANSMISSION_LINE' and CHILD_ENTITY='CONDUCTOR_MASTER' and PARENT_ENTITY_ATTRIBUTE='ConductorType' and CHILD_ENTITY_ATTRIBUTE='Id'";
                        
                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<AcTransLineCktCondTypeForeign> lineCondTypesForeign = new List<AcTransLineCktCondTypeForeign> ();
                        while (reader.Read())
                        {
                            AcTransLineCktCondTypeForeign lineCondTypeForeign = new AcTransLineCktCondTypeForeign();
                            lineCondTypeForeign.WebUatId = reader.GetInt32(0);
                            lineCondTypeForeign.AcTransLineCktWebUatId = reader.GetInt32(1);
                            lineCondTypeForeign.CondTypeWebUatId = reader.GetInt32(2);
                            lineCondTypesForeign.Add(lineCondTypeForeign);
                        }

                        reader.Dispose();

                        return lineCondTypesForeign;
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
