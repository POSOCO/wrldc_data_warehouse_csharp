using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class BusReactor
    {
        public int BusReactorId { get; set; }
        [Required]
        public string Name { get; set; }
        public int BusReactorNumber { get; set; }
        public decimal MvarCapacity { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }

        public Bus Bus { get; set; }
        public int BusId { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public IList<BusReactorOwner> BusReactorOwners { get; set; }

        public int WebUatId { get; set; }
    }
}
