using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignBuses
    {
        public async Task ImportForeignBuses(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            BusExtract busExtract = new BusExtract();
            List<BusForeign> busForeigns = busExtract.ExtractBusesForeign(oracleConnStr);

            LoadBus loadBus = new LoadBus();
            foreach (BusForeign busForeign in busForeigns)
            {
                Bus insertedBus = await loadBus.LoadSingleAsync(_context, busForeign, opt);
            }
        }
    }
}
