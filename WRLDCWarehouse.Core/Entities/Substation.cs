using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class Substation
    {
        public int SubstationId { get; set; }
        [Required]
        public string Name { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public MajorSubstation MajorSubstation { get; set; }
        public int MajorSubstationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public IList<SubstationOwner> SubstationOwners { get; set; }

        [Required]
        public string Classification { get; set; }
        [Required]
        public string BusbarScheme { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }

        public int WebUatId { get; set; }
    }
}
