namespace WRLDCWarehouse.Core.Entities
{
    public class LineReactorOwner
    {
        public LineReactor LineReactor { get; set; }
        public int LineReactorId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
