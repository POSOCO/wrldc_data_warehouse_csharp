using System;
using System.Collections.Generic;
using System.Text;
using WRLDCWarehouse.Core.Frequency;
using WRLDCWarehouse.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadFrequencySummary
    {
        public async Task LoadDailyFrequencySummary(WRLDCWarehouseDbContext _context, MartDailyFrequencySummary frequencySummary)
        {
            // check if frequencySummary already exists for the date and delete it
            MartDailyFrequencySummary existingFreqSumm = await _context.MartDailyFrequencySummaries.SingleOrDefaultAsync(mdfs => mdfs.DataDate == frequencySummary.DataDate);
            if (existingFreqSumm != null)
            {
                _context.MartDailyFrequencySummaries.Remove(existingFreqSumm);
                // await _context.SaveChangesAsync();
            }

            // insert the new frequency Summary
            _context.Add(frequencySummary);
            await _context.SaveChangesAsync();
        }
    }
}
