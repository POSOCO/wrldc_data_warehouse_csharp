using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class ConductorType
    {
        public int ConductorTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public int WebUatId { get; set; }
    }
}
