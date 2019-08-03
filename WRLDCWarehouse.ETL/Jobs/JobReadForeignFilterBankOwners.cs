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
    public class JobReadForeignFilterBankOwners
    {
        public async Task ImportForeignFilterBankOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            FilterBankOwnerExtract fbOwnerExtract = new FilterBankOwnerExtract();
            List<FilterBankOwnerForeign> fbOwnersForeign = fbOwnerExtract.ExtractFilterBankOwnersForeign(oracleConnStr);

            LoadFilterBankOwner loadFbOwner = new LoadFilterBankOwner();
            foreach (FilterBankOwnerForeign fbOwnerForeign in fbOwnersForeign)
            {
                FilterBankOwner insertedFbOwner = await loadFbOwner.LoadSingleAsync(_context, _log, fbOwnerForeign, opt);
            }
        }
    }
}
