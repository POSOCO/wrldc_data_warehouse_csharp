using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class BayType
    {
        public int WebUatId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
