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
    public class JobReadForeignBusReactors
    {
        public async Task ImportForeignBusReactors(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            BusReactorExtract brExtract = new BusReactorExtract();
            List<BusReactorForeign> brsForeignList = brExtract.ExtractBusReactorsForeign(oracleConnStr);

            LoadBusReactor loadBr = new LoadBusReactor();
            foreach (BusReactorForeign brForeign in brsForeignList)
            {
                BusReactor insertedBr = await loadBr.LoadSingleAsync(_context, _log, brForeign, opt);
            }
        }
    }
}
