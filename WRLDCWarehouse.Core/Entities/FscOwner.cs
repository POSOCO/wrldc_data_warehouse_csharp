namespace WRLDCWarehouse.Core.Entities
{
    public class FscOwner
    {
        public Fsc Fsc { get; set; }
        public int FscId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
