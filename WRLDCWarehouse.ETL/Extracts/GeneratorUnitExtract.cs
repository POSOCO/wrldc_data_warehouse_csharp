using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class GeneratorUnitExtract
    {
        public List<GeneratorUnitForeign> ExtractGeneratorUnitsForeign(string oracleConnString)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"select ID, UNIT_NAME, UNIT_NUMBER, FK_GENERATING_STATION, FK_STAGE, GENERATING_VOLTAGE_KV, GENERATION_HIGH_VOLTAGE_KV, 
                                            MVA_CAPACITY, INSTALLED_CAPACITY, DATETIME_OF_COMMISSIONING, DATETIME_OF_COD, 
                                            DATETIME_OF_DECOMMISSIONING from GENERATING_UNIT 
                                            where :id=1 and UNIT_NAME IS NOT NULL and ID IS NOT NULL and FK_GENERATING_STATION IS NOT NULL and FK_STAGE IS NOT NULL";

                        // Assign id parameter
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<GeneratorUnitForeign> genUnitsForeign = new List<GeneratorUnitForeign>();
                        while (reader.Read())
                        {
                            GeneratorUnitForeign genUnitForeign = new GeneratorUnitForeign();
                            genUnitForeign.WebUatId = reader.GetInt32(0);
                            genUnitForeign.Name = reader.GetString(1);
                            genUnitForeign.UnitNumber = reader.GetInt32(2);
                            genUnitForeign.GeneratingStationWebUatId = reader.GetInt32(3);
                            genUnitForeign.GeneratorStageWebUatId = reader.GetInt32(4);
                            genUnitForeign.GenVoltageKV = reader.GetDecimal(5);
                            genUnitForeign.GenHighVoltageKV = reader.GetDecimal(6);
                            genUnitForeign.MvaCapacity = reader.GetDecimal(7);
                            genUnitForeign.InstalledCapacity = reader.GetDecimal(8);
                            genUnitForeign.CommDateTime = reader.GetDateTime(9);
                            genUnitForeign.CodDateTime = reader.GetDateTime(10);
                            genUnitForeign.DeCommDateTime = reader.GetDateTime(11);
                            genUnitsForeign.Add(genUnitForeign);
                        }

                        reader.Dispose();

                        return genUnitsForeign;
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
