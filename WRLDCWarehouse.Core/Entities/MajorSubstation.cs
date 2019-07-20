using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class MajorSubstation
    {
        public int MajorSusbstationId { get; set; }
        [Required]
        public string Name { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public int WebUatId { get; set; }
    }
}
