namespace WRLDCWarehouse.Core.Entities
{
    public class FilterBankOwner
    {
        public FilterBank FilterBank { get; set; }
        public int FilterBankId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
