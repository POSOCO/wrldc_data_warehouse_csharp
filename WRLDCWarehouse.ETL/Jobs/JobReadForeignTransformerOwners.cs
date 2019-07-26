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
    public class JobReadForeignTransformerOwners
    {
        public async Task ImportForeignTransformerOwners(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            TransformerOwnerExtract trOwnerExtract = new TransformerOwnerExtract();
            List<TransformerOwnerForeign> trOwnersForeign = trOwnerExtract.ExtractTransformerOwnersForeign(oracleConnStr);

            LoadTransformerOwner loadTrOwner = new LoadTransformerOwner();
            foreach (TransformerOwnerForeign trOwnerForeign in trOwnersForeign)
            {
                TransformerOwner insertedTrOwner = await loadTrOwner.LoadSingleAsync(_context, _log, trOwnerForeign, opt);
            }
        }
    }
}
