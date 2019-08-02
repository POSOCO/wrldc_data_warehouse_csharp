using System.Collections.Generic;

namespace WRLDCWarehouse.Core.Entities
{
    public class FilterBank
    {
        public int FilterBankId { get; set; }

        // Name is null in some rows of vendor db, hence unable to keep Required attribute
        public string Name { get; set; }

        public Region Region { get; set; }
        public int RegionId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public bool IsSwitchable { get; set; }
        public string FilterBankNumber { get; set; }

        public IList<FilterBankOwner> FilterBankOwners { get; set; }

        public int WebUatId { get; set; }
    }
}
