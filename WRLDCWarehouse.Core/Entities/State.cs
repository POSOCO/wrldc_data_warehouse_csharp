using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class State
    {
        public int StateId { get; set; }

        public Region Region { get; set; }
        public int RegionId { get; set; }

        [Required]
        public string ShortName { get; set; }
        [Required]
        public string FullName { get; set; }

        public int WebUatId { get; set; }
    }
}
