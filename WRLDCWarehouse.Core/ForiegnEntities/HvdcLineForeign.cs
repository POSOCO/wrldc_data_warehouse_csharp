namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class HvdcLineForeign
    {
        public int WebUatId { get; set; }
        public string Name { get; set; }
        public int FromSSWebUatId { get; set; }
        public int ToSSWebUatId { get; set; }
        public int VoltLevelWebUatId { get; set; }
        public int FromStateWebUatId { get; set; }
        public int ToStateWebUatId { get; set; }
    }    
}
