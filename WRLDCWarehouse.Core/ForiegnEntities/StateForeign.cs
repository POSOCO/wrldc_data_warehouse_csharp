using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WRLDCWarehouse.Core.ForiegnEntities
{
    public class StateForeign
    {
        public int WebUatId { get; set; }
        public int RegionWebUatId { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
    }
}
