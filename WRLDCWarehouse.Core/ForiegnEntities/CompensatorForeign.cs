using System;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class CompensatorForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int CompensatorTypeWebUatId { get; set; }
        public int SubstationWebUatId { get; set; }
        public int StateWebUatId { get; set; }
        public int CompensatorNumber { get; set; }
        public int AttachElementType { get; set; }
        public int AttachElementWebUatId { get; set; }
        public int PercVariableComp { get; set; }
        public int PercFixedComp { get; set; }
        public int LineReactanceOhms { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DeCommDate { get; set; }
    }
}
