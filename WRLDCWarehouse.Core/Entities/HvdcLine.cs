using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class HvdcLine
    {
        public int HvdcLineId { get; set; }
        [Required]
        public string Name { get; set; }

        public Substation FromSubstation { get; set; }
        public int FromSubstationId { get; set; }

        public Substation ToSubstation { get; set; }
        public int ToSubstationId { get; set; }

        public VoltLevel VoltLevel { get; set; }
        public int VoltLevelId { get; set; }

        public State FromState { get; set; }
        public int FromStateId { get; set; }

        public State ToState { get; set; }
        public int ToStateId { get; set; }

        public int WebUatId { get; set; }
    }
}
