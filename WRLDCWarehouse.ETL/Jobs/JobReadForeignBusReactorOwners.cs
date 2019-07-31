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
    public class JobReadForeignBusReactorOwners
    {
        public async Task ImportForeignBusReactorOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            BusReactorOwnerExtract brOwnerExtract = new BusReactorOwnerExtract();
            List<BusReactorOwnerForeign> brOwnersForeign = brOwnerExtract.ExtractBusReactorOwnersForeign(oracleConnStr);

            LoadBusReactorOwner loadBrOwner = new LoadBusReactorOwner();
            foreach (BusReactorOwnerForeign brOwnerForeign in brOwnersForeign)
            {
                BusReactorOwner insertedBrOwner = await loadBrOwner.LoadSingleAsync(_context, _log, brOwnerForeign, opt);
            }
        }
    }
}
