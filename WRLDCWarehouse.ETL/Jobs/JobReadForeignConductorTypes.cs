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
    public class JobReadForeignConductorTypes
    {
        public async Task ImportForeignConductorTypes(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            ConductorTypeExtract condTypeExtract = new ConductorTypeExtract();
            List<ConductorType> condTypes = condTypeExtract.ExtractConductorTypes(oracleConnStr);

            LoadConductorType loadCondType = new LoadConductorType();
            foreach (ConductorType condType in condTypes)
            {
                ConductorType insertedCondType = await loadCondType.LoadSingleAsync(_context, _log, condType, opt);
            }
        }
    }
}
