using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WRLDCWarehouse.Core.Frequency;

namespace WRLDCWarehouse.Data
{
    public class WRLDCWarehouseDbContext : DbContext
    {
        public DbSet<RawFrequency> RawFrequencies { get; set; }
        public DbSet<MartDailyFrequencySummary> MartDailyFrequencySummaries { get; set; }

        // use connection string here if not working when used in startup.cs page - https://github.com/nagasudhirpulla/open_shift_scheduler/blob/master/OpenShiftScheduler/Data/ShiftScheduleDbContext.cs
        public WRLDCWarehouseDbContext(DbContextOptions<WRLDCWarehouseDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RawFrequency>()
            .HasIndex(rf => rf.DataTime)
            .IsUnique();

            builder.Entity<MartDailyFrequencySummary>()
            .HasIndex(mfs => mfs.DataDate)
            .IsUnique();
        }

    }
}
