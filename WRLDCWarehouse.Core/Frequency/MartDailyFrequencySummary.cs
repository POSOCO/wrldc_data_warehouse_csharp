using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WRLDCWarehouse.Core.Frequency
{
    public class MartDailyFrequencySummary
    {
        // efcore date type column https://stackoverflow.com/questions/5658216/entity-framework-code-first-date-field-creation, https://stackoverflow.com/questions/33762292/store-only-date-in-database-not-time-portion-c-sharp/33805300
        // https://www.meziantou.net/entity-framework-core-specifying-data-type-length-and-precision.htm
        // make sure DateKey is set unique in fluent api

        public int MartDailyFrequencySummaryId { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime DataDate { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal AverageFrequency { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal StandardDeviationFrequency { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal FVIFrequency { get; set; }

        [Required, Column(TypeName = "decimal(5,3)")]
        public decimal NumOutOfIEGCHrs { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercLess48_8 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercLess49 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercLess49_2 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercLess49_5 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercLess49_7 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercLess49_9 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercGreatEq49_9LessEq50_05 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal PercGreat50_05 { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal MaxFrequency { get; set; }

        public DateTime MaxFrequencyTime { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal MinFrequency { get; set; }

        public DateTime MinFrequencyTime { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal MaxBlkFreq { get; set; }

        [Required, Column(TypeName = "decimal(4,2)")]
        public decimal MinBlkFreq { get; set; }

    }
}
