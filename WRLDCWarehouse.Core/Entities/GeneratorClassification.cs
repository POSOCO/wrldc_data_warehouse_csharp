using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class GeneratorClassification
    {
        public int GeneratorClassificationId { get; set; }
        [Required]
        public string Name { get; set; }
        public int WebUatId { get; set; }
    }
}
