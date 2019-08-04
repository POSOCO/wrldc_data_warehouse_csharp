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
    public class JobReadForeignHvdcPoles
    {
        public async Task ImportForeignHvdcPoles(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            HvdcPoleExtract hvdcPoleExtract = new HvdcPoleExtract();
            List<HvdcPoleForeign> hvdcPoleForeignList = hvdcPoleExtract.ExtractHvdcPoleForeign(oracleConnStr);

            LoadHvdcPole loadHvdcPole = new LoadHvdcPole();
            foreach (HvdcPoleForeign lineForeign in hvdcPoleForeignList)
            {
                HvdcPole insertedPole = await loadHvdcPole.LoadSingleAsync(_context, _log, lineForeign, opt);
            }
        }
    }
}
