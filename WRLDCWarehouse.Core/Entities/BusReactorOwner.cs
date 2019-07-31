namespace WRLDCWarehouse.Core.Entities
{
    public class BusReactorOwner
    {
        public BusReactor BusReactor { get; set; }
        public int BusReactorId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
