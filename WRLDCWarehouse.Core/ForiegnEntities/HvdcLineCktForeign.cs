using System;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class HvdcLineCktForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int CktNumber { get; set; }
        public int hvdcLineWebUatId { get; set; }
        public int FromSubstationWebUatId { get; set; }
        public int ToSubstationWebUatId { get; set; }
        public int FromBusWebUatId { get; set; }
        public int ToBusWebUatId { get; set; }
        public int NumConductorsPerCkt { get; set; }
        public decimal Length { get; set; }
        public decimal ThermalLimitMVA { get; set; }
        public DateTime FtcDate { get; set; }
        public DateTime TrialOperationDate { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CODDate { get; set; }
        public DateTime DeCommDate { get; set; }
    }
}
