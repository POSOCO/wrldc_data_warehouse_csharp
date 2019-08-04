namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class BayForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int SourceEntityWebUatId { get; set; }
        public string SourceEntityType { get; set; }
        public string SourceEntityName { get; set; }
        public int DestEntityWebUatId { get; set; }
        public string DestEntityType { get; set; }
        public string DestEntityName { get; set; }
        public string BayNumber { get; set; }
        public int BayTypeWebUatId { get; set; }
        public int VoltageWebUatId { get; set; }
        public int SubstationWebUatId { get; set; }
    }
}
