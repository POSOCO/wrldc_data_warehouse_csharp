using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class LineReactor
    {
        public int LineReactorId { get; set; }

        [Required]
        public string Name { get; set; }
        public decimal MvarCapacity { get; set; }
        public DateTime CommDate { get; set; }
        public DateTime CodDate { get; set; }
        public DateTime DecommDate { get; set; }
        public bool IsConvertible { get; set; }
        public bool IsSwitchable { get; set; }

        public AcTransLineCkt AcTransLineCkt { get; set; }
        public int AcTransLineCktId { get; set; }

        public Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public IList<LineReactorOwner> LineReactorOwners { get; set; }

        public int WebUatId { get; set; }

    }
}
