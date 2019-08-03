using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class HvdcLineCkt
    {
        public int HvdcLineCktId { get; set; }
        [Required]
        public string Name { get; set; }
        public string CktNumber { get; set; }

        public HvdcLine HvdcLine { get; set; }
        public int HvdcLineId { get; set; }

        public Bus FromBus { get; set; }
        public int FromBusId { get; set; }

        public Bus ToBus { get; set; }
        public int ToBusId { get; set; }

        public int NumConductorsPerCkt { get; set; }
        public decimal Length { get; set; }
        public decimal ThermalLimitMVA { get; set; }
        public DateTime FtcDate { get; set; }
        public DateTime TrialOperationDate { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DeCommDate { get; set; }

        public IList<HvdcLineCktOwner> HvdcLineCktOwners { get; set; }

        public int WebUatId { get; set; }

    }
}
