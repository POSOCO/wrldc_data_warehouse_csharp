using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class AcTransmissionLineCircuitForeign
    {
        // todo create seperate entity named AcTransLineCktCondTypeForeign
        public int WebUatId { get; set; }
        public int Name { get; set; }
        public int AcTransLineWebUatId { get; set; }
        public int VoltLevelWebUatId { get; set; }
        public int CktNumber { get; set; }
        public decimal Length { get; set; }
        public decimal ThermalLimitMVA { get; set; }
        public decimal SIL { get; set; }
        public int FromBusWebUatId { get; set; }
        public int ToBusWebUatId { get; set; }
        public DateTime FtcDate { get; set; }
        public DateTime TrialOperationDate { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CODDate { get; set; }
        public DateTime DeCommDate { get; set; }
    }
}
