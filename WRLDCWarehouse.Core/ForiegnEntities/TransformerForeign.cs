using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class TransformerForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public string StationType { get; set; }
        public int HVStationWebUatId { get; set; }
        public int TransformerNumber { get; set; }
        public int TransTypeWebUatId { get; set; }
        public decimal MVACapacity { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }
        public int StateWebUatId { get; set; }
        public int HighVoltLevelWebUatId { get; set; }
        public int LowVoltLevelWebUatId { get; set; }
    }
}