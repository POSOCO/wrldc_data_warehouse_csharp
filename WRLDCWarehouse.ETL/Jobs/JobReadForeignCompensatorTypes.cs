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
    public class JobReadForeignCompensatorTypes
    {
        public async Task ImportForeignCompensatorTypes(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            CompensatorTypeExtract compensatorTypeExtract = new CompensatorTypeExtract();
            List<CompensatorType> compensatorTypes = compensatorTypeExtract.ExtractCompensatorTypes(oracleConnStr);

            LoadCompensatorType loadCompensatorType = new LoadCompensatorType();
            foreach (CompensatorType compensatorType in compensatorTypes)
            {
                CompensatorType insertedCompensatorType = await loadCompensatorType.LoadSingleAsync(_context, _log, compensatorType, opt);
            }
        }
    }
}
