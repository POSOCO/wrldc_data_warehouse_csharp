namespace WRLDCWarehouse.Core.Entities
{
    public class GenerationTypeFuel
    {
        public GenerationType GenerationType { get; set; }
        public int GenerationTypeId { get; set; }

        public Fuel Fuel { get; set; }
        public int FuelId { get; set; }

        public int WebUatId { get; set; }
    }
}
