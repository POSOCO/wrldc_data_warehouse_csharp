using System.ComponentModel.DataAnnotations;

namespace WRLDCWarehouse.Core.Entities
{
    public class Fuel
    {
        public int FuelId { get; set; }
        [Required]
        public string Name { get; set; }

        public int WebUatId { get; set; }
    }
}
