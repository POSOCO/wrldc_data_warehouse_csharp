using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class AcTransmissionLine
    {
        public int AcTransmissionLineId { get; set; }
        [Required]
        public int Name { get; set; }

        public Substation FromSubstation { get; set; }
        public int FromSubstationId { get; set; }

        public Substation ToSubstation { get; set; }
        public int ToSubstationId { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }
        
        public int WebUatId { get; set; }
    }
}
