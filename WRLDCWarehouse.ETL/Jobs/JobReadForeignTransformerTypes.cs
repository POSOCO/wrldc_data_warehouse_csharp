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
    public class JobReadForeignTransformerTypes
    {
        public async Task ImportForeignTransformerTypes(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            TransformerTypeExtract transTypeExtract = new TransformerTypeExtract();
            List<TransformerType> transTypes = transTypeExtract.ExtractTransformerTypes(oracleConnStr);

            LoadTransformerType loadTrType = new LoadTransformerType();
            foreach (TransformerType trType in transTypes)
            {
                TransformerType insertedTrType = await loadTrType.LoadSingleAsync(_context, _log, trType, opt);
            }
        }
    }
}
