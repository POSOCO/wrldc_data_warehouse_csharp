using System;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class LineReactorForeign
    {
        public int WebUatId { get; set; }
        public int AcTransLineCktWebUatId { get; set; }
        public int SubstationWebUatId { get; set; }
        public decimal MvarCapacity { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }
        public int StateWebUatId { get; set; }
        public int IsConvertible { get; set; }
        public int IsSwitchable { get; set; }
        public string Name { get; set; }
    }
}
