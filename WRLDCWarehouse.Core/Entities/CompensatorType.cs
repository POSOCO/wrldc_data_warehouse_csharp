using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class CompensatorType
    {
        public int CompensatorTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public int WebUatId { get; set; }
    }
}
