using System;
using System.Collections.Generic;

namespace WRLDCWarehouse.Core.Entities
{
    public class Fsc
    {
        public int FscId { get; set; }
        public int WebUatId { get; set; }
        public string Name { get; set; }

        public AcTransLineCkt AcTransLineCkt { get; set; }
        public int AcTransLineCktId { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public int PercComp { get; set; }
        public int LineReactance { get; set; }
        public int CapacitiveReactance { get; set; }
        public int RatedMvarPhase { get; set; }
        public int RatedCurrentAmps { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DeCommDate { get; set; }

        public IList<FscOwner> FscOwners { get; set; }
    }
}
