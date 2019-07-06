using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WRLDCWarehouse.Core.Frequency
{
    // We can see how have data provider and app secrets setup to this project by referring to other project at m4-07 (44th) video of ASP.Net Core Fundamentals, Pluralsight course
    // make sure DataTime is set unique in fluent api
    public class RawFrequency
    {
        // set precision and scale https://stackoverflow.com/questions/3504660/decimal-precision-and-scale-in-ef-code-first
        // 23.5141 has a precision of 6 and a scale of 4. - https://www.postgresql.org/docs/9.6/datatype-numeric.html
        public int RawFrequencyId { get; set; }

        [Required]
        public DateTime DataTime { get; set; }

        [Required, Column(TypeName = "decimal(5,3)")]
        public decimal Frequency { get; set; }
    }
}
