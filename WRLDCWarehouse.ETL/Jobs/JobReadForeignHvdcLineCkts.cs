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
    public class JobReadForeignHvdcLineCkts
    {
        public async Task ImportForeignHvdcLineCkts(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            HvdcLineCktExtract hvdcLineCktExtract = new HvdcLineCktExtract();
            List<HvdcLineCktForeign> hvdcLineCktForeignList = hvdcLineCktExtract.ExtractHvdcLineCktForeign(oracleConnStr);

            LoadHvdcLineCkt loadHvdcLineCkt = new LoadHvdcLineCkt();
            foreach (HvdcLineCktForeign cktForeign in hvdcLineCktForeignList)
            {
                HvdcLineCkt insertedCkt = await loadHvdcLineCkt.LoadSingleAsync(_context, _log, cktForeign, opt);
            }
        }
    }
}
