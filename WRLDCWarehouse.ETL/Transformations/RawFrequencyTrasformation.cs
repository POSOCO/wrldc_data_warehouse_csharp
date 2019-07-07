using System;
using System.Collections.Generic;
using System.Linq;
using WRLDCWarehouse.Core.Frequency;
using WRLDCWarehouse.ETL.StatHelpers;

namespace WRLDCWarehouse.ETL.Transformations
{
    public class RawFrequencyTrasformation
    {
        public MartDailyFrequencySummary TransformDayFreq(IEnumerable<RawFrequency> rawDayFreqs)
        {
            // we are assuming that the samples are in sorted time order
            if (rawDayFreqs.Count() == 0)
            {
                return null;
            }
            MartDailyFrequencySummary martDailyFrequencySummary = new MartDailyFrequencySummary();
            martDailyFrequencySummary.DataDate = rawDayFreqs.First().DataTime.Date;

            IEnumerable<decimal> rawFreqVals = rawDayFreqs.Select(rf => rf.Frequency);
            int rawFreqValsCount = rawFreqVals.Count();

            // average freq
            decimal avgFreq = rawFreqVals.Average();

            // max freq
            decimal maxFreq = rawFreqVals.Max();

            // max freq time
            int maxFreqInd = rawFreqVals.MaxIndex();
            DateTime maxFreqTime = rawDayFreqs.ElementAt(maxFreqInd).DataTime;

            // min freq
            decimal minFreq = rawFreqVals.Min();

            // min freq time
            int minFreqInd = rawFreqVals.MinIndex();
            DateTime minFreqTime = rawDayFreqs.ElementAt(minFreqInd).DataTime;

            // standard deviation of freq
            decimal stdFreq = rawFreqVals.StandardDeviation();

            List<decimal> freqBlks = rawFreqVals.GetBlockValues();
            // max block freq
            decimal maxBlkFreq = freqBlks.Max();

            // min block freq
            decimal minBlkFreq = freqBlks.Min();

            // percentage values less than 48.8
            decimal percLess48_8 = rawFreqVals.Where(rf => rf < 48.8m).Count() * 100 / rawFreqValsCount;

            // percentage values less than 49
            decimal percLess49 = rawFreqVals.Where(rf => rf < 49m).Count() * 100 / rawFreqValsCount;

            // percentage values less than 49.2
            decimal percLess49_2 = rawFreqVals.Where(rf => rf < 49.2m).Count() * 100 / rawFreqValsCount;

            // percentage values less than 49.5
            decimal percLess49_5 = rawFreqVals.Where(rf => rf < 49.5m).Count() * 100 / rawFreqValsCount;

            // percentage values less than 49.7
            decimal percLess49_7 = rawFreqVals.Where(rf => rf < 49.7m).Count() * 100 / rawFreqValsCount;

            // percentage values less than 49.9
            decimal percLess49_9 = rawFreqVals.Where(rf => rf < 49.9m).Count() * 100 / rawFreqValsCount;

            // percentage values betwen 49.9 and 50.05
            decimal percGreatEq49_9LessEq50_05 = rawFreqVals.Where(rf => rf >= 49.9m && rf <= 50.5m).Count() * 100 / rawFreqValsCount;

            // percentage values more than 50.05
            decimal percGreat50_05 = rawFreqVals.Where(rf => rf >= 50.5m).Count() * 100 / rawFreqValsCount;

            // calculate number of freq is out of IEGC band
            decimal NumOutOfIEGCHrs = (decimal)((rawDayFreqs.Last().DataTime - rawDayFreqs.First().DataTime).TotalHours * (double)percGreatEq49_9LessEq50_05 * 0.01);

            //calculate fvi of freq
            decimal fvi = rawFreqVals.GetFVI();


            martDailyFrequencySummary.AverageFrequency = avgFreq;

            martDailyFrequencySummary.MaxFrequency = maxFreq;
            martDailyFrequencySummary.MaxFrequencyTime = maxFreqTime;

            martDailyFrequencySummary.MinFrequency = minFreq;
            martDailyFrequencySummary.MinFrequencyTime = minFreqTime;

            martDailyFrequencySummary.MaxBlkFreq = maxBlkFreq;
            martDailyFrequencySummary.MinBlkFreq = minBlkFreq;

            martDailyFrequencySummary.NumOutOfIEGCHrs = NumOutOfIEGCHrs;
            martDailyFrequencySummary.FVIFrequency = fvi;
            martDailyFrequencySummary.StandardDeviationFrequency = stdFreq;

            martDailyFrequencySummary.PercLess49 = percLess49;
            martDailyFrequencySummary.PercLess49_2 = percLess49_2;
            martDailyFrequencySummary.PercLess49_5 = percLess49_5;
            martDailyFrequencySummary.PercLess49_7 = percLess49_7;
            martDailyFrequencySummary.PercLess48_8 = percLess48_8;
            martDailyFrequencySummary.PercLess49_9 = percLess49_9;
            martDailyFrequencySummary.PercGreatEq49_9LessEq50_05 = percGreatEq49_9LessEq50_05;
            martDailyFrequencySummary.PercGreat50_05 = percGreat50_05;

            return martDailyFrequencySummary;
        }
    }
}
