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
    public class JobReadForeignHvdcLines
    {
        public async Task ImportForeignHvdcLines(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            HvdcLineExtract hvdcLineExtract = new HvdcLineExtract();
            List<HvdcLineForeign> hvdcLineForeignList = hvdcLineExtract.ExtractHvdcLineForeign(oracleConnStr);

            LoadHvdcLine loadHvdcLine = new LoadHvdcLine();
            foreach (HvdcLineForeign lineForeign in hvdcLineForeignList)
            {
                HvdcLine insertedLine = await loadHvdcLine.LoadSingleAsync(_context, _log, lineForeign, opt);
            }
        }
    }
}
