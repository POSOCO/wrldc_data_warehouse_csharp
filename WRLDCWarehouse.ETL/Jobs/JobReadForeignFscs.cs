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
    public class JobReadForeignFscs
    {
        public async Task ImportForeignFscs(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            FscExtract fscExtract = new FscExtract();
            List<FscForeign> fscForeignList = fscExtract.ExtractFscForeign(oracleConnStr);

            LoadFsc loadFsc = new LoadFsc();
            foreach (FscForeign lineForeign in fscForeignList)
            {
                Fsc insertedFsc = await loadFsc.LoadSingleAsync(_context, _log, lineForeign, opt);
            }
        }
    }
}
