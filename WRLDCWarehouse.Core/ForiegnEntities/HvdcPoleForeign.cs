using System;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class HvdcPoleForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int PoleNumber { get; set; }
        public int SubstationWebUatId { get; set; }
        public int StateWebUatId { get; set; }
        public int VoltLevelWebUatId { get; set; }
        public string PoleType { get; set; }
        public decimal MaxFiringAngleDegrees { get; set; }
        public decimal MinFiringAngleDegrees { get; set; }
        public decimal ThermalLimitMVA { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DeCommDate { get; set; }
    }
}
