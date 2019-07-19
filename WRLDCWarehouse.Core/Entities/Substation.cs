using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class Substation
    {
        public int SubstationId { get; set; }
        public string Name { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public MajorSubstation MajorSubstation { get; set; }
        public int MajorSubstationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public IList<SubstationOwner> SubstationOwners { get; set; }

        public string Classification { get; set; }
        public string BusbarScheme { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }

        public int WebUatId { get; set; }
    }
}
