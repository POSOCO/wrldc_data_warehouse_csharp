﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using WRLDCWarehouse.Core.Frequency;

namespace WRLDCWarehouse.ETL.Extracts
{
    public class RawFreqExtract
    {
        public List<RawFrequency> ExtractRawFreqs(string oracleConnString, DateTime startTime, DateTime endTime)
        {
            using (OracleConnection con = new OracleConnection(oracleConnString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT DATE_KEY, TIME_KEY, FREQ_VAL FROM STG_SCADA_FREQUENCY_NLDC where DATE_KEY between :from_date_key and :to_date_key and ISDELETED = :isdeleted order by DATE_KEY, TIME_KEY";

                        // Assign id parameter
                        OracleParameter from_date_key = new OracleParameter("from_date_key", startTime.ToString("yyyyMMdd"));
                        OracleParameter to_date_key = new OracleParameter("to_date_key", endTime.ToString("yyyyMMdd"));
                        cmd.Parameters.Add(from_date_key);
                        cmd.Parameters.Add(to_date_key);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        List<RawFrequency> rawFreqs = new List<RawFrequency>();
                        while (reader.Read())
                        {
                            RawFrequency rawFreq = new RawFrequency();
                            //todo parse datetime
                            string dateKey = reader.GetString(0);
                            string timeKey = reader.GetString(0);
                            DateTime dataTime;
                            bool isValidDataTime = DateTime.TryParseExact($"{dateKey} {timeKey}", "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataTime);

                            if (isValidDataTime)
                            {
                                rawFreq.DataTime = dataTime;
                                rawFreq.Frequency = reader.GetDecimal(2);
                                rawFreqs.Add(rawFreq);
                            }
                            else
                            {
                                Console.WriteLine("Invalid datetime ${dateKey} ${timeKey}");
                            }
                        }

                        reader.Dispose();

                        return rawFreqs;
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
