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
    public class JobReadForeignCompensatorOwners
    {
        public async Task ImportForeignCompensatorOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            CompensatorOwnerExtract compensatorOwnerExtract = new CompensatorOwnerExtract();
            List<CompensatorOwnerForeign> compensatorOwnerForeignList = compensatorOwnerExtract.ExtractCompensatorOwnersForeign(oracleConnStr);

            LoadCompensatorOwner loadCompensatorOwner = new LoadCompensatorOwner();
            foreach (CompensatorOwnerForeign compensatorOwnerForeign in compensatorOwnerForeignList)
            {
                CompensatorOwner insertedCompensatorOwner = await loadCompensatorOwner.LoadSingleAsync(_context, _log, compensatorOwnerForeign, opt);
            }
        }
    }
}
