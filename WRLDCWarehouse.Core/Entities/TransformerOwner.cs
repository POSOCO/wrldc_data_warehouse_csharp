namespace WRLDCWarehouse.Core.Entities
{
    public class TransformerOwner
    {
        public Transformer Transformer { get; set; }
        public int TransformerId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
