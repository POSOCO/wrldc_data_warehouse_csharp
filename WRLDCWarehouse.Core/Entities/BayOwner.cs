namespace WRLDCWarehouse.Core.Entities
{
    public class BayOwner
    {
        public Bay Bay { get; set; }
        public int BayId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
