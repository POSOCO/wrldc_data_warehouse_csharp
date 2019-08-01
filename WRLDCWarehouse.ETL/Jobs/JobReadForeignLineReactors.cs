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
    public class JobReadForeignLineReactors
    {
        public async Task ImportForeignLineReactors(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            LineReactorExtract lrExtract = new LineReactorExtract();
            List<LineReactorForeign> lrsForeignList = lrExtract.ExtractLineReactorsForeign(oracleConnStr);

            LoadLineReactor loadLr = new LoadLineReactor();
            foreach (LineReactorForeign lrForeign in lrsForeignList)
            {
                LineReactor insertedLr = await loadLr.LoadSingleAsync(_context, _log, lrForeign, opt);
            }
        }
    }
}
