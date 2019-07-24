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
    public class JobReadForeignGeneratingStationOwners
    {
        public async Task ImportForeignGeneratingStationOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            GeneratingStationOwnerExtract genStationOwnerExtract = new GeneratingStationOwnerExtract();
            List<GeneratingStationOwnerForeign> genStationOwnersForeign = genStationOwnerExtract.ExtractGeneratingStationOwnersForeign(oracleConnStr);

            LoadGeneratingStationOwner loadGenStationOwner = new LoadGeneratingStationOwner();
            foreach (GeneratingStationOwnerForeign genStationOwnerForeign in genStationOwnersForeign)
            {
                GeneratingStationOwner insertedGenStationOwner = await loadGenStationOwner.LoadSingleAsync(_context, _log, genStationOwnerForeign, opt);
            }
        }
    }
}
