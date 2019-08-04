using System;
using System.Collections.Generic;

namespace WRLDCWarehouse.Core.Entities
{
    public class Compensator
    {
        public int CompensatorId { get; set; }
        public int WebUatId { get; set; }
        public string Name { get; set; }

        public CompensatorType CompensatorType { get; set; }
        public int CompensatorTypeId { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public int AttachElementType { get; set; }
        public int AttachElementId { get; set; }

        public string CompensatorNumber { get; set; }
        public int PercVariableComp { get; set; }
        public int PercFixedComp { get; set; }
        public int LineReactanceOhms { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DeCommDate { get; set; }

        public IList<CompensatorOwner> CompensatorOwners { get; set; }
    }
}
