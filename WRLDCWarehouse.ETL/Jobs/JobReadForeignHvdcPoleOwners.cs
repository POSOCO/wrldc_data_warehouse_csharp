using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignHvdcPoleOwners
    {
        public async Task ImportForeignHvdcPoleOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            HvdcPoleOwnerExtract hvdcPoleOwnerExtract = new HvdcPoleOwnerExtract();
            List<HvdcPoleOwnerForeign> hvdcPoleOwnerForeignList = hvdcPoleOwnerExtract.ExtractHvdcPoleOwnersForeign(oracleConnStr);

            LoadHvdcPoleOwner loadHvdcPoleOwner = new LoadHvdcPoleOwner();
            foreach (HvdcPoleOwnerForeign poleOwnerForeign in hvdcPoleOwnerForeignList)
            {
                HvdcPoleOwner insertedPoleOwner = await loadHvdcPoleOwner.LoadSingleAsync(_context, _log, poleOwnerForeign, opt);
            }
        }
    }
}
