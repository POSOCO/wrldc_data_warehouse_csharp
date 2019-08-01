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
    public class JobReadForeignLineReactorOwners
    {
        public async Task ImportForeignLineReactorOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            LineReactorOwnerExtract lrOwnerExtract = new LineReactorOwnerExtract();
            List<LineReactorOwnerForeign> lrOwnersForeign = lrOwnerExtract.ExtractLineReactorOwnersForeign(oracleConnStr);

            LoadLineReactorOwner loadLrOwner = new LoadLineReactorOwner();
            foreach (LineReactorOwnerForeign lrOwnerForeign in lrOwnersForeign)
            {
                LineReactorOwner insertedLrOwner = await loadLrOwner.LoadSingleAsync(_context, _log, lrOwnerForeign, opt);
            }
        }
    }
}
