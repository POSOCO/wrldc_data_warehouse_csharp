using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class VoltLevel
    {
        public int VoltLevelId { get; set; }
        [Required]
        public string Name { get; set; }
        public string EntityType { get; set; }

        public int WebUatId { get; set; }

    }
}
