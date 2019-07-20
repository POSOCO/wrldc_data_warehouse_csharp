using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class Bus
    {
        // bus number and associate substation should be unique
        public int BusId { get; set; }
        public int Name { get; set; }
        public string BusNumber { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public int WebUatId { get; set; }

    }
}
