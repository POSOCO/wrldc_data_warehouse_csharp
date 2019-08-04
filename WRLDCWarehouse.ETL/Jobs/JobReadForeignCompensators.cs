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
    public class JobReadForeignCompensators
    {
        public async Task ImportForeignCompensators(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            CompensatorExtract compensatorExtract = new CompensatorExtract();
            List<CompensatorForeign> compensatorForeignList = compensatorExtract.ExtractCompensatorForeign(oracleConnStr);

            LoadCompensator loadCompensator = new LoadCompensator();
            foreach (CompensatorForeign compensatorForeign in compensatorForeignList)
            {
                Compensator insertedCompensator = await loadCompensator.LoadSingleAsync(_context, _log, compensatorForeign, opt);
            }
        }
    }
}
