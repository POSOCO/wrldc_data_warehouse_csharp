using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class AcTransmissionLineForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int FromSSWebUatId { get; set; }
        public int ToSSWebUatId { get; set; }
        public int VoltLevelWebUatId { get; set; }
    }
}
