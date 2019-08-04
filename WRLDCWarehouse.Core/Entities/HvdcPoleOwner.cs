namespace WRLDCWarehouse.Core.Entities
{
    public class HvdcPoleOwner
    {
        public HvdcPole HvdcPole { get; set; }
        public int HvdcPoleId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
