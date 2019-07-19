using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class SubstationForeign
    {
        // make default bus bar scheme, lat, long as NA
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public double VoltLevelWebUatId { get; set; }
        public int MajorSubstationWebUatId { get; set; }
        public string StateWebUatId { get; set; }
        public string Classification { get; set; }
        public string BusbarScheme { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime DecommDate { get; set; }
    }
}
