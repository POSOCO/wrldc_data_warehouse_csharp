using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class GeneratingStation
    {
        public int GeneratingStationId { get; set; }

        [Required]
        public string Name { get; set; }

        public GeneratorClassification GeneratorClassification { get; set; }
        public int GeneratorClassificationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public GenerationType GenerationType { get; set; }
        public int GenerationTypeId { get; set; }

        public Fuel Fuel { get; set; }
        // making fuel Id nullable since vendor doesnot comply to non null fuel types
        public int? FuelId { get; set; }

        public IList<GeneratingStationOwner> GeneratingStationOwners { get; set; }

        public int WebUatId { get; set; }
    }
}
