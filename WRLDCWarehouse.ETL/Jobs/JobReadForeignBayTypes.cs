using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignBayTypes
    {
        public async Task ImportForeignFuels(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            BayTypeExtract bayTypeExtract = new BayTypeExtract();
            List<BayType> bayTypes = bayTypeExtract.ExtractBays(oracleConnStr);

            LoadBayType loadBayType = new LoadBayType();
            foreach (BayType bayType in bayTypes)
            {
                BayType insertedBayType = await loadBayType.LoadSingleAsync(_context, _log, bayType, opt);
            }
        }
    }
}
