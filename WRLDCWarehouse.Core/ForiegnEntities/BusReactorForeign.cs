using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class BusReactorForeign
    {
        public int WebUatId { get; set; }
        public int BusReactorNumber { get; set; }
        public int BusWebUatId { get; set; }
        public int SubstationWebUatId { get; set; }
        public decimal MvarCapacity { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }
        public int StateWebUatId { get; set; }
        public string Name { get; set; }
    }
}
