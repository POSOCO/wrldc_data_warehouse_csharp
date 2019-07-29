using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.ETL.Transformations;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WRLDCWarehouse.Core.Frequency;
using System.Threading.Tasks;
using WRLDCWarehouse.ETL.Extracts;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobTransformRawFrequency
    {
        public async Task CreateFrequencySummaryForDate(WRLDCWarehouseDbContext _context, DateTime dateTime)
        {
            // get the raw frequency data for the required date
            List<RawFrequency> rawFreqs = await _context.RawFrequencies.Where(rf => rf.DataTime.Date == dateTime.Date).ToListAsync();

            // transform the raw frequency data
            RawFrequencyTrasformation rawFrequencyTrasformation = new RawFrequencyTrasformation();
            MartDailyFrequencySummary martDailyFrequencySummary = rawFrequencyTrasformation.TransformDayFreq(rawFreqs);

            // load it to the mart
            LoadFrequencySummary loadFrequencySummary = new LoadFrequencySummary();
            await loadFrequencySummary.LoadDailyFrequencySummary(_context, martDailyFrequencySummary);
        }

        public async Task CreateFrequencySummaryForDates(WRLDCWarehouseDbContext _context, DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime.Date >= endDateTime.Date)
            {
                return;
            }

            // run the job for each day
            foreach (DateTime day in DateUtils.EachDay(startDateTime, endDateTime))
            {
                await CreateFrequencySummaryForDate(_context, day);
            }
        }

        public void ExtractFrequenciesForDates(string oracleConnStr, DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime.Date >= endDateTime.Date)
            {
                return;
            }

            // run the job for each day
            foreach (DateTime day in DateUtils.EachDay(startDateTime, endDateTime))
            {
                RawFreqExtract rawFreqExtract = new RawFreqExtract();
                rawFreqExtract.ExtractRawFreqs(oracleConnStr, startDateTime, endDateTime);
            }
        }

        public async Task ProcessFrequenciesForDates(WRLDCWarehouseDbContext _context, string oracleConnStr, DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime.Date >= endDateTime.Date)
            {
                return;
            }

            // run the job for each day
            foreach (DateTime day in DateUtils.EachDay(startDateTime, endDateTime))
            {
                RawFreqExtract rawFreqExtract = new RawFreqExtract();
                List<RawFrequency> rawFreqs = rawFreqExtract.ExtractRawFreqs(oracleConnStr, day, day);

                // transform the raw frequency data
                RawFrequencyTrasformation rawFrequencyTrasformation = new RawFrequencyTrasformation();
                MartDailyFrequencySummary martDailyFrequencySummary = rawFrequencyTrasformation.TransformDayFreq(rawFreqs);

                // load it to the mart
                LoadFrequencySummary loadFrequencySummary = new LoadFrequencySummary();
                await loadFrequencySummary.LoadDailyFrequencySummary(_context, martDailyFrequencySummary);
            }
        }
    }
}
