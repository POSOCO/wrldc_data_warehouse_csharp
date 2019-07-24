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
    public class JobReadForeignGeneratorUnits
    {
        public async Task ImportForeignGeneratorUnits(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            GeneratorUnitExtract genUnitExtract = new GeneratorUnitExtract();
            List<GeneratorUnitForeign> genUnitsForeign = genUnitExtract.ExtractGeneratorUnitsForeign(oracleConnStr);

            LoadGeneratorUnit loadGenUnit = new LoadGeneratorUnit();
            foreach (GeneratorUnitForeign genUnitForeign in genUnitsForeign)
            {
                GeneratorUnit insertedGenUnit = await loadGenUnit.LoadSingleAsync(_context, _log, genUnitForeign, opt);
            }
        }
    }
}
