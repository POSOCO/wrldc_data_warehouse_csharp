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
    public class JobReadForeignGeneratorStages
    {
        public async Task ImportForeignGeneratorStages(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            GeneratorStageExtract genStageExtract = new GeneratorStageExtract();
            List<GeneratorStageForeign> genStagesForeign = genStageExtract.ExtractGeneratorStagesForeign(oracleConnStr);

            LoadGeneratorStage loadGenStage = new LoadGeneratorStage();
            foreach (GeneratorStageForeign genStageForeign in genStagesForeign)
            {
                GeneratorStage insertedGenStage = await loadGenStage.LoadSingleAsync(_context, _log, genStageForeign, opt);
            }
        }
    }
}
