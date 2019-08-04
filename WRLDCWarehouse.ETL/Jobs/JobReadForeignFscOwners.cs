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
    public class JobReadForeignFscOwners
    {
        public async Task ImportForeignFscOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            FscOwnerExtract fscOwnerExtract = new FscOwnerExtract();
            List<FscOwnerForeign> fscOwnerForeignList = fscOwnerExtract.ExtractFscOwnersForeign(oracleConnStr);

            LoadFscOwner loadFscOwner = new LoadFscOwner();
            foreach (FscOwnerForeign fscOwnerForeign in fscOwnerForeignList)
            {
                FscOwner insertedFscOwner = await loadFscOwner.LoadSingleAsync(_context, _log, fscOwnerForeign, opt);
            }
        }
    }
}
