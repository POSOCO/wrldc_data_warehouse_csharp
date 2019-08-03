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
    public class JobReadForeignHvdcLineCktOwners
    {
        public async Task ImportForeignHvdcLineCktOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            HvdcLineCktOwnerExtract hvdcLineCktOwnerExtract = new HvdcLineCktOwnerExtract();
            List<HvdcLineCktOwnerForeign> hvdcLineCktOwnerForeignList = hvdcLineCktOwnerExtract.ExtractHvdcLineCktOwnersForeign(oracleConnStr);

            LoadHvdcLineCktOwner loadHvdcLineCktOwner = new LoadHvdcLineCktOwner();
            foreach (HvdcLineCktOwnerForeign cktOwnerForeign in hvdcLineCktOwnerForeignList)
            {
                HvdcLineCktOwner insertedCktOwner = await loadHvdcLineCktOwner.LoadSingleAsync(_context, _log, cktOwnerForeign, opt);
            }
        }
    }
}
