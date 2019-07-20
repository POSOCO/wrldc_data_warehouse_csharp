using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using WRLDCWarehouse.ETL.Extracts;

namespace TestSpace
{
    class Program
    {
        static void Main(string[] args)
        {

            string oracleWebUatConnString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={Environment.GetEnvironmentVariable("REPORTING_DB_HOST")})(PORT={Environment.GetEnvironmentVariable("REPORTING_DB_PORT")})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={Environment.GetEnvironmentVariable("REPORTING_DB_SERVICENAME")})));User Id={Environment.GetEnvironmentVariable("REPORTING_DB_WEBUAT_USERNAME")};Password={Environment.GetEnvironmentVariable("REPORTING_DB_WEBUAT_PASSWORD")};";

            //TestExtractVoltageLevels(oracleWebUatConnString);
            //TestExtractOwners(oracleWebUatConnString);
            //TestExtractRegions(oracleWebUatConnString);
            //TestExtractStates(oracleWebUatConnString);
            //TestExtractMajorSubstations(oracleWebUatConnString);
            //TestExtractSubstations(oracleWebUatConnString);
            //TestExtractSubstationOwners(oracleWebUatConnString);
            //TestExtractConductorTypes(oracleWebUatConnString);
            //TestExtractLineConductorTypes(oracleWebUatConnString);
            //TestExtractBuses(oracleWebUatConnString);
        }


        public static void TestExtractVoltageLevels(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extarcting Voltage Levels");
            VoltLevelExtract voltLevelExtract = new VoltLevelExtract();
            List<VoltLevel> volts = voltLevelExtract.ExtractVoltageLevels(oracleWebUatConnString);
            Console.WriteLine(volts);
        }

        public static void TestExtractOwners(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extarcting Owners");
            OwnerExtract ownerExtract = new OwnerExtract();
            List<Owner> owners = ownerExtract.ExtractOwners(oracleWebUatConnString);
            Console.WriteLine(owners);
        }

        public static void TestExtractRegions(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extarcting Regions");
            RegionExtract regionExtract = new RegionExtract();
            List<Region> regions = regionExtract.ExtractRegions(oracleWebUatConnString);
            Console.WriteLine(regions);
        }

        public static void TestExtractStates(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extarcting States");
            StateExtract regionExtract = new StateExtract();
            List<StateForeign> statesForeign = regionExtract.ExtractStatesForeign(oracleWebUatConnString);
            Console.WriteLine(statesForeign);
        }

        public static void TestExtractMajorSubstations(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extracting Major Substations");
            MajorSubstationExtract majorSSExtract = new MajorSubstationExtract();
            List<MajorSubstationForeign> substationsForeign = majorSSExtract.ExtractMajorSubstationsForeign(oracleWebUatConnString);
            Console.WriteLine(substationsForeign);
        }

        public static void TestExtractSubstations(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extracting Substations");
            SubstationExtract ssExtract = new SubstationExtract();
            List<SubstationForeign> substationsForeign = ssExtract.ExtractSubstationsForeign(oracleWebUatConnString);
            Console.WriteLine(substationsForeign);
        }

        public static void TestExtractSubstationOwners(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extracting Substation Owners");
            SubstationOwnerExtract ssOwnerExtract = new SubstationOwnerExtract();
            List<SubstationOwnerForeign> substationOwnersForeign = ssOwnerExtract.ExtractSubstationOwnersForeign(oracleWebUatConnString);
            Console.WriteLine(substationOwnersForeign);
        }

        public static void TestExtractConductorTypes(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extracting Conductor Types");
            ConductorTypeExtract condTypeExtract = new ConductorTypeExtract();
            List<ConductorType> condTypes = condTypeExtract.ExtractConductorTypes(oracleWebUatConnString);
            Console.WriteLine(condTypes);
        }

        public static void TestExtractBuses(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extracting Buses");
            BusExtract busExtract = new BusExtract();
            List<BusForeign> busesForeign = busExtract.ExtractBusesForeign(oracleWebUatConnString);
            Console.WriteLine(busesForeign);
        }

        public static void TestExtractLineConductorTypes(string oracleWebUatConnString)
        {
            Console.WriteLine("Test - Extracting Line Conductor Types of foreign database");
            ConductorTypeExtract condTypeExtract = new ConductorTypeExtract();
            List<AcTransLineCktCondTypeForeign> lineCondTypes = condTypeExtract.ExtractLineConductorTypesForeign(oracleWebUatConnString);
            Console.WriteLine(lineCondTypes);
        }

        public static void ConnTest(string conString)
        {
            Console.WriteLine("Oracle Data fetch test...");
            //Create a connection to Oracle			
            // string conString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={Environment.GetEnvironmentVariable("REPORTING_DB_HOST")})(PORT={Environment.GetEnvironmentVariable("REPORTING_DB_PORT")})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={Environment.GetEnvironmentVariable("REPORTING_DB_SERVICENAME")})));User Id={Environment.GetEnvironmentVariable("REPORTING_DB_USERNAME")};Password={Environment.GetEnvironmentVariable("REPORTING_DB_PASSWORD")};";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select NAME from DIM_MAJOR_SUBSTATION where :id=1";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", 1);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Major Substation Name: " + reader.GetString(0) + "\n");
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
