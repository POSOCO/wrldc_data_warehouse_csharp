namespace WRLDCWarehouse.Core.Entities
{
    public class CompensatorOwner
    {
        public Compensator Compensator { get; set; }
        public int CompensatorId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
