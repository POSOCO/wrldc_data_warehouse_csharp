namespace WRLDCWarehouse.Core.Entities
{
    public class GeneratingStationOwner
    {
        public GeneratingStation GeneratingStation { get; set; }
        public int GeneratingStationId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
