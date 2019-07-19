using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class SubstationOwner
    {
        //foreign keys to be set via fluent api
        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }

    }
}
