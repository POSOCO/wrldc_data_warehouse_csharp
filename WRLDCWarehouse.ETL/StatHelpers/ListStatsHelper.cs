using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRLDCWarehouse.ETL.StatHelpers
{
    // https://stackoverflow.com/questions/462699/how-do-i-get-the-index-of-the-highest-value-in-an-array-using-linq/9251836
    // https://rextester.com/ - run c# online
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-and-call-a-custom-extension-method - creating our own extension methods
    public static class ListStatsHelper
    {
        public static int MaxIndex<T>(this IEnumerable<T> sequence) where T : IComparable<T>
        {
            int maxIndex = -1;
            T maxValue = default(T); // Immediately overwritten anyway

            int index = 0;
            foreach (T value in sequence)
            {
                if (value.CompareTo(maxValue) > 0 || maxIndex == -1)
                {
                    maxIndex = index;
                    maxValue = value;
                }
                index++;
            }
            return maxIndex;
        }

        public static int MinIndex<T>(this IEnumerable<T> sequence) where T : IComparable<T>
        {
            int minIndex = -1;
            T minValue = default(T); // Immediately overwritten anyway

            int index = 0;
            foreach (T value in sequence)
            {
                if (value.CompareTo(minValue) < 0 || minIndex == -1)
                {
                    minIndex = index;
                    minValue = value;
                }
                index++;
            }
            return minIndex;
        }

        public static decimal StandardDeviation(this IEnumerable<decimal> sequence)
        {
            // https://stackoverflow.com/questions/5336457/how-to-calculate-a-standard-deviation-array

            decimal sd = default(decimal); // Immediately overwritten anyway

            decimal average = sequence.Average();
            decimal sumOfSquaresOfDifferences = sequence.Select(val => (val - average) * (val - average)).Sum();
            sd = (decimal)Math.Sqrt((double)sumOfSquaresOfDifferences / sequence.Count());

            return sd;
        }

        public static List<decimal> GetBlockValues(this IEnumerable<decimal> sequence)
        {
            // https://stackoverflow.com/questions/5336457/how-to-calculate-a-standard-deviation-array
            int seqCount = sequence.Count();

            List<decimal> sequenceBlks = new List<decimal>();
            int blkLen = 15;

            for (int blkIter = 0; blkIter < Math.Ceiling((decimal)sequence.Count() / blkLen); blkIter++)
            {
                int seqIterStart = blkIter * blkLen;
                int seqIterEnd = (blkIter + 1) * blkLen - 1;
                if (seqIterEnd >= seqCount)
                {
                    seqIterEnd = seqCount - 1;
                }
                decimal blkVal = sequence.Skip(seqIterStart).Take(seqIterEnd - seqIterStart + 1).Average();
                sequenceBlks.Add(blkVal);
            }

            return sequenceBlks;
        }

        public static decimal GetFVI(this IEnumerable<decimal> sequence)
        {
            int seqCount = sequence.Count();

            List<decimal> sequenceBlks = new List<decimal>();

            int midFreq = 50;
            decimal fvi = (decimal)sequence.Select(s => Math.Pow(Math.Abs((double)s - midFreq), 2)).Sum() * 10 / seqCount;

            return fvi;
        }
    }
}
