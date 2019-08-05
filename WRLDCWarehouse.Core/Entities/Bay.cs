using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class Bay
    {
        // Name should be unique, 
        // (BayTypeId, SourceEntityId, SourceEntityType, DestEntityId, DestEntityType) should be unique
        public int BayId { get; set; }

        [Required]
        public string Name { get; set; }
        public string BayNumber { get; set; }

        public int SourceEntityId { get; set; }
        [Required]
        public string SourceEntityType { get; set; }
        public string SourceEntityName { get; set; }
        public int? DestEntityId { get; set; }
        public string DestEntityType { get; set; }
        public string DestEntityName { get; set; }

        public BayType BayType { get; set; }
        public int BayTypeId { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public IList<BayOwner> BayOwners { get; set; }

        public int WebUatId { get; set; }

    }
}