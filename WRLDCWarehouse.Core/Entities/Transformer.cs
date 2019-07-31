using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class Transformer
    {
        public int TransformerId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string StationType { get; set; }

        public VoltLevel HighVoltLevel { get; set; }
        public int HighVoltLevelId { get; set; }

        public VoltLevel LowVoltLevel { get; set; }
        // using nullable low voltage levels due to vendor non compliance
        public int? LowVoltLevelId { get; set; }

        public MajorSubstation HvSubstation { get; set; }
        public int? HvSubstationId { get; set; }

        public GeneratingStation HvGeneratingStation { get; set; }
        public int? HvGeneratingStationId { get; set; }

        public int TransformerNumber { get; set; }

        public TransformerType TransformerType { get; set; }
        public int TransformerTypeId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public decimal MVACapacity { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }

        public IList<TransformerOwner> TransformerOwners { get; set; }

        public int WebUatId { get; set; }
    }
}
