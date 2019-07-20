using System;
using System.Collections.Generic;
using System.Text;

namespace WRLDCWarehouse.Core.Entities
{
    public class AcTransLineCktOwner
    {
        public AcTransLineCkt AcTransLineCkt { get; set; }
        public int AcTransLineCktId { get; set; }

        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public int WebUatId { get; set; }
    }
}
