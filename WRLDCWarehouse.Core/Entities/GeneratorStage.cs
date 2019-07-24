using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class GeneratorStage
    {
        // STAGE_NAME and GENERATING_STATION should be unique
        public int GeneratorStageId { get; set; }
        [Required]
        public string Name { get; set; }

        public GeneratingStation GeneratingStation { get; set; }
        public int GeneratingStationId { get; set; }

        public int WebUatId { get; set; }
    }
}
