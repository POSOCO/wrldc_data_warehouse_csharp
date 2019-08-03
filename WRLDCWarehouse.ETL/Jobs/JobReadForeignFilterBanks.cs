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
    public class JobReadForeignFilterBanks
    {
        public async Task ImportForeignFilterBanks(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            FilterBankExtract fbExtract = new FilterBankExtract();
            List<FilterBankForeign> fbForeignList = fbExtract.ExtractFilterBanksForeign(oracleConnStr);

            LoadFilterBank loadFb = new LoadFilterBank();
            foreach (FilterBankForeign fbForeign in fbForeignList)
            {
                FilterBank insertedSS = await loadFb.LoadSingleAsync(_context, _log, fbForeign, opt);
            }
        }
    }
}
