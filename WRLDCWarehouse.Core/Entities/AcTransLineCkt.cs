using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class AcTransLineCkt
    {
        // volt level is redundant since we can get that through from and to ss volt levels
        // Bus 1 and Bus 2 is pending

        public int AcTransLineCktId { get; set; }
        [Required]
        public string Name { get; set; }
        public string CktNumber { get; set; }

        public AcTransmissionLine AcTransmissionLine { get; set; }
        public int AcTransmissionLineId { get; set; }

        public ConductorType ConductorType { get; set; }
        public int ConductorTypeId { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public IList<AcTransLineCktOwner> AcTransLineCktOwners { get; set; }

        public decimal Length { get; set; }
        public decimal ThermalLimitMVA { get; set; }
        public decimal SIL { get; set; }
        public DateTime FtcDate { get; set; }
        public DateTime TrialOperationDate { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CODDate { get; set; }
        public DateTime DeCommDate { get; set; }

        public int WebUatId { get; set; }

    }
}
