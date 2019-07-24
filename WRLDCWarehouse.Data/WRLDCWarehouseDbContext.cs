using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WRLDCWarehouse.Core.Frequency;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.Data
{
    public class WRLDCWarehouseDbContext : DbContext
    {
        public DbSet<RawFrequency> RawFrequencies { get; set; }
        public DbSet<MartDailyFrequencySummary> MartDailyFrequencySummaries { get; set; }

        // Entites
        public DbSet<Region> Regions { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<VoltLevel> VoltLevels { get; set; }
        public DbSet<ConductorType> ConductorTypes { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<GeneratorClassification> GeneratorClassifications { get; set; }
        public DbSet<GenerationType> GenerationTypes { get; set; }
        public DbSet<GenerationTypeFuel> GenerationTypeFuels { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<MajorSubstation> MajorSubstations { get; set; }
        public DbSet<Substation> Substations { get; set; }
        public DbSet<SubstationOwner> SubstationOwners { get; set; }
        public DbSet<AcTransmissionLine> AcTransmissionLines { get; set; }
        public DbSet<AcTransLineCkt> AcTransLineCkts { get; set; }
        public DbSet<AcTransLineCktOwner> AcTransLineCktOwners { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<GeneratingStation> GeneratingStations { get; set; }
        public DbSet<GeneratingStationOwner> GeneratingStationOwners { get; set; }
        public DbSet<GeneratorStage> GeneratorStages { get; set; }
        public DbSet<GeneratorUnit> GeneratorUnits { get; set; }


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

            // Region Settings - ShortName, FullName, WebUatId are unique
            builder.Entity<Region>()
            .HasIndex(r => r.ShortName)
            .IsUnique();
            builder.Entity<Region>()
            .HasIndex(r => r.FullName)
            .IsUnique();
            builder.Entity<Region>()
            .HasIndex(r => r.WebUatId)
            .IsUnique();

            // Owner Settings - Name, WebUatId are unique. Todo take up with vendor and remove owne id 20 and then we can make name unique
            //builder.Entity<Owner>()
            //.HasIndex(o => o.Name)
            //.IsUnique();
            builder.Entity<Owner>()
            .HasIndex(o => o.WebUatId)
            .IsUnique();

            // VoltLevel settings - Name, WebUatId are unique
            builder.Entity<VoltLevel>()
            .HasIndex(v => v.Name)
            .IsUnique();
            builder.Entity<VoltLevel>()
             .HasIndex(v => v.WebUatId)
             .IsUnique();

            // Conductor Type settings - Name, WebUatId are unique
            builder.Entity<ConductorType>()
            .HasIndex(ct => ct.Name)
            .IsUnique();
            builder.Entity<ConductorType>()
             .HasIndex(ct => ct.WebUatId)
             .IsUnique();

            // States settings - FullName, ShortName, WebUatId are unique
            builder.Entity<State>()
            .HasIndex(s => s.FullName)
            .IsUnique();
            builder.Entity<State>()
            .HasIndex(s => s.ShortName)
            .IsUnique();
            builder.Entity<State>()
             .HasIndex(s => s.WebUatId)
             .IsUnique();

            // Major Substation settings - Name, WebUatId are unique
            builder.Entity<MajorSubstation>()
            .HasIndex(ms => ms.Name)
            .IsUnique();
            builder.Entity<MajorSubstation>()
             .HasIndex(ms => ms.WebUatId)
             .IsUnique();

            // Substation settings - (Name, Classification), WebUatId are unique, default value of bus bar scheme is NA
            builder.Entity<Substation>().HasIndex(b => new { b.Name, b.Classification }).IsUnique();
            builder.Entity<Substation>()
             .HasIndex(ss => ss.WebUatId)
             .IsUnique();
            builder.Entity<Substation>()
            .Property(ss => ss.BusbarScheme)
            .IsRequired()
            .HasDefaultValue("NA");

            // Many to Many relationship of Substation owners
            builder.Entity<SubstationOwner>().HasKey(ss => new { ss.SubstationId, ss.OwnerId });

            builder.Entity<SubstationOwner>()
                .HasOne(so => so.Substation)
                .WithMany(ss => ss.SubstationOwners)
                .HasForeignKey(so => so.SubstationId);

            builder.Entity<SubstationOwner>()
                .HasOne(so => so.Owner)
                .WithMany()
                .HasForeignKey(so => so.OwnerId);

            // Ac Transmission Line settings - Name, WebUatId are unique, optionally we can keep from and to substation combination also as unique
            builder.Entity<AcTransmissionLine>()
            .HasIndex(actl => actl.Name)
            .IsUnique();
            builder.Entity<AcTransmissionLine>()
             .HasIndex(actl => actl.WebUatId)
             .IsUnique();

            // Ac Transmission Line Circuit settings - Name, WebUatId are unique, combination of AcTransmissionLineId and circuit number is unique
            builder.Entity<AcTransLineCkt>()
            .HasIndex(ckt => ckt.Name)
            .IsUnique();
            builder.Entity<AcTransLineCkt>()
             .HasIndex(ckt => ckt.WebUatId)
             .IsUnique();
            builder.Entity<AcTransLineCkt>().HasIndex(ckt => new { ckt.AcTransLineCktId, ckt.CktNumber }).IsUnique();

            // Many to Many relationship of Transmisssion line Circuit owners
            builder.Entity<AcTransLineCktOwner>().HasKey(cktOwn => new { cktOwn.AcTransLineCktId, cktOwn.OwnerId });

            builder.Entity<AcTransLineCktOwner>()
                .HasOne(cktOwn => cktOwn.AcTransLineCkt)
                .WithMany(ckt => ckt.AcTransLineCktOwners)
                .HasForeignKey(cktOwn => cktOwn.AcTransLineCktId);

            builder.Entity<AcTransLineCktOwner>()
                .HasOne(cktOwn => cktOwn.Owner)
                .WithMany()
                .HasForeignKey(cktOwn => cktOwn.OwnerId);

            // Bus settings - WebUatId is unique.Name is unique, but we are not keeping it since vendor db is not compliant. Combination of bus number and associate substation is unique, but we are unable to implement this as vendor is not compliant
            //builder.Entity<Bus>()
            //.HasIndex(b => b.Name)
            //.IsUnique();
            builder.Entity<Bus>()
             .HasIndex(b => b.WebUatId)
             .IsUnique();
            // builder.Entity<Bus>().HasIndex(b => new { b.SubstationId, b.BusNumber }).IsUnique();

            // Fuel Settings - Name, WebUatId are unique
            builder.Entity<Fuel>()
            .HasIndex(f => f.Name)
            .IsUnique();
            builder.Entity<Fuel>()
            .HasIndex(f => f.WebUatId)
            .IsUnique();

            // GenerationType Settings - Name, WebUatId are unique
            builder.Entity<GenerationType>()
            .HasIndex(gt => gt.Name)
            .IsUnique();
            builder.Entity<GenerationType>()
            .HasIndex(gt => gt.WebUatId)
            .IsUnique();

            // Many to Many relationship of GenerationTypeFuel
            builder.Entity<GenerationTypeFuel>().HasKey(gtf => new { gtf.GenerationTypeId, gtf.FuelId });

            builder.Entity<GenerationTypeFuel>()
                .HasOne(gtf => gtf.GenerationType)
                .WithMany()
                .HasForeignKey(gtf => gtf.GenerationTypeId);

            builder.Entity<GenerationTypeFuel>()
                .HasOne(gtf => gtf.Fuel)
                .WithMany()
                .HasForeignKey(gtf => gtf.FuelId);

            // Fuel GeneratorClassification - Name, WebUatId are unique
            builder.Entity<GeneratorClassification>()
            .HasIndex(gc => gc.Name)
            .IsUnique();
            builder.Entity<GeneratorClassification>()
            .HasIndex(gc => gc.WebUatId)
            .IsUnique();

            // GeneratingStation settings - (Name, GeneratorClassificationId, StateId), WebUatId are unique. Only (Name, GeneratorClassificationId) should be unique, but vendor db is not compliant
            builder.Entity<GeneratingStation>().HasIndex(gs => new { gs.Name, gs.GeneratorClassificationId, gs.StateId }).IsUnique();
            builder.Entity<GeneratingStation>()
             .HasIndex(gs => gs.WebUatId)
             .IsUnique();

            // Many to Many relationship of GeneratingStation Owners
            builder.Entity<GeneratingStationOwner>().HasKey(gso => new { gso.GeneratingStationId, gso.OwnerId });

            builder.Entity<GeneratingStationOwner>()
                .HasOne(gso => gso.GeneratingStation)
                .WithMany(gs => gs.GeneratingStationOwners)
                .HasForeignKey(so => so.GeneratingStationId);

            builder.Entity<GeneratingStationOwner>()
                .HasOne(gso => gso.Owner)
                .WithMany()
                .HasForeignKey(so => so.OwnerId);

            // GeneratorStage settings - (Name, GeneratingStationId), WebUatId are unique.
            builder.Entity<GeneratorStage>().HasIndex(gs => new { gs.Name, gs.GeneratingStationId}).IsUnique();
            builder.Entity<GeneratorStage>()
             .HasIndex(gs => gs.WebUatId)
             .IsUnique();

            // GeneratorUnit settings - (UnitNumber, GeneratingStationId, GeneratorStageId), WebUatId are unique.
            builder.Entity<GeneratorUnit>().HasIndex(gu => new { gu.UnitNumber, gu.GeneratingStationId, gu.GeneratorStageId }).IsUnique();
            builder.Entity<GeneratorUnit>()
             .HasIndex(gs => gs.WebUatId)
             .IsUnique();
        }

    }
}
