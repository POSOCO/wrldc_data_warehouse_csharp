using System;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class FscForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int AcTransLineCktWebUatId { get; set; }
        public int SubstationWebUatId { get; set; }
        public int StateWebUatId { get; set; }
        public int PercComp { get; set; }
        public int LineReactance { get; set; }
        public int CapacitiveReactance { get; set; }
        public int RatedMvarPhase { get; set; }
        public int RatedCurrentAmps { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DeCommDate { get; set; }
    }
}
