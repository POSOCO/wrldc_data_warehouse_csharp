using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class HvdcPole
    {
        public int HvdcPoleId { get; set; }
        [Required]
        public string Name { get; set; }
        public string PoleNumber { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public string PoleType { get; set; }
        public decimal MaxFiringAngleDegrees { get; set; }
        public decimal MinFiringAngleDegrees { get; set; }
        public decimal ThermalLimitMVA { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DeCommDate { get; set; }

        public IList<HvdcPoleOwner> HvdcPoleOwners { get; set; }

        public int WebUatId { get; set; }

    }
}
