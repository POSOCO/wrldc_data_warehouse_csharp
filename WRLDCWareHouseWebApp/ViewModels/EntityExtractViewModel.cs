using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWareHouseWebApp.ViewModels
{
    public class EntityExtractViewModel
    {
        [DisplayName("Extraction Strategy")]
        public EntityWriteOption EntityWriteOption { get; set; }
    }
}
