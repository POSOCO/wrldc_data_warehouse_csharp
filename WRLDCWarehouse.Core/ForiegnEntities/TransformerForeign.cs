using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class TransformerForeign
    {
        public int WebUatId { get; set; }

        public string Name { get; set; }   
        
        public int HVStateWebUatId { get; set; }

        public int CircuitNumber { get; set; }

        //public TransformerTypeForeign TransformerType {get ; set ; }
        public int TypeGCICT { get; set; }

        public double MVACapacity { get; set; }

        public DateTime CommDate { get; set; }

        public DateTime CodDate { get; set; }
        
        public DateTime DecommDate { get; set; }

        public int LocationWebUatId { get; set; }

        public string StationType { get; set; }

        public int HighVoltageLevel { get; set; }

        public int LowVoltageLevel { get; set; }
    }
}