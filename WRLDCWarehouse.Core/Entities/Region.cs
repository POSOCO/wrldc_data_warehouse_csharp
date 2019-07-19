using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class Region
    {
        public int RegionId { get; set; }
        public string Fullname { get; set; }
        public string ShortName { get; set; }
        public int WebUatId { get; set; }
    }
}
