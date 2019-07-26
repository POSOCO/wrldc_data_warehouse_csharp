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
    public class JobReadForeignTransformers
    {
        public async Task ImportForeignTransformers(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            TransformerExtract trExtract = new TransformerExtract();
            List<TransformerForeign> trForeignList = trExtract.ExtractTransformersForeign(oracleConnStr);

            LoadTransformer loadTr = new LoadTransformer();
            foreach (TransformerForeign trForeign in trForeignList)
            {
                Transformer insertedTr = await loadTr.LoadSingleAsync(_context, _log, trForeign, opt);
            }
        }
    }
}
