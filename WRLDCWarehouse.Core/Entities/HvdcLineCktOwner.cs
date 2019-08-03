namespace WRLDCWarehouse.Core.Entities
{
    public class HvdcLineCktOwner
    {
        public HvdcLineCkt HvdcLineCkt { get; set; }
        public int HvdcLineCktId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
