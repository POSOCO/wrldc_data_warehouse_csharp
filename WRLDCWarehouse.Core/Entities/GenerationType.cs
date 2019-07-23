using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class GenerationType
    {
        public int GenerationTypeId { get; set; }
        [Required]
        public string Name { get; set; }

        public int WebUatId { get; set; }
    }
}
