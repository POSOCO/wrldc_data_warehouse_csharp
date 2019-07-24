using System;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class GeneratorUnitForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int UnitNumber { get; set; }
        public int GeneratingStationWebUatId { get; set; }
        public int GeneratorStageWebUatId { get; set; }
        public decimal GenVoltageKV { get; set; }
        public decimal GenHighVoltageKV { get; set; }
        public decimal MvaCapacity { get; set; }
        public decimal InstalledCapacity { get; set; }
        public DateTime CommDateTime { get; set; }
        public DateTime CodDateTime { get; set; }
        public DateTime DeCommDateTime { get; set; }
    }
}
