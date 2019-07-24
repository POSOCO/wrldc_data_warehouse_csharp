using System;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class GeneratorUnit
    {
        // UnitNumber, STAGE_NAME and GENERATING_STATION should be unique
        public int GeneratorUnitId { get; set; }
        [Required]
        public string Name { get; set; }
        public string UnitNumber { get; set; }

        public GeneratingStation GeneratingStation { get; set; }
        public int GeneratingStationId { get; set; }

        public GeneratorStage GeneratorStage { get; set; }
        public int GeneratorStageId { get; set; }

        public decimal GenVoltageKV { get; set; }
        public decimal GenHighVoltageKV { get; set; }
        public decimal MvaCapacity { get; set; }
        public decimal InstalledCapacity { get; set; }
        public DateTime CommDateTime { get; set; }
        public DateTime CodDateTime { get; set; }
        public DateTime DeCommDateTime { get; set; }

        public int WebUatId { get; set; }
    }
}
