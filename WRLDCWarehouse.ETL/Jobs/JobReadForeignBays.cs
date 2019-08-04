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
    public class JobReadForeignBays
    {
        public async Task ImportForeignBays(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            BayExtract bayExtract = new BayExtract();
            List<BayForeign> bayForeigns = bayExtract.ExtractBaysForeign(oracleConnStr);

            LoadBay loadBay = new LoadBay();
            foreach (BayForeign bayForeign in bayForeigns)
            {
                Bay insertedBay = await loadBay.LoadSingleAsync(_context, _log, bayForeign, opt);
            }
        }
    }
}
