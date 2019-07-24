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
    public class JobReadForeignGeneratorClassifications
    {
        public async Task ImportForeignGenClassifications(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            GeneratorClassificationExtract genClassificationExtract = new GeneratorClassificationExtract();
            List<GeneratorClassification> genClassifications = genClassificationExtract.ExtractGeneratorClassifications(oracleConnStr);

            LoadGeneratorClassification loadGenClassification = new LoadGeneratorClassification();
            foreach (GeneratorClassification genClassification in genClassifications)
            {
                GeneratorClassification insertedGenClassification = await loadGenClassification.LoadSingleAsync(_context, _log, genClassification, opt);
            }
        }
    }
}
