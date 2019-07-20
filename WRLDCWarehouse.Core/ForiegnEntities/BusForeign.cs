using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class BusForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int BusNumber { get; set; }
        public int VoltageWebUatId { get; set; }
        public int AssSubstationWebUatId { get; set; }
    }
}
