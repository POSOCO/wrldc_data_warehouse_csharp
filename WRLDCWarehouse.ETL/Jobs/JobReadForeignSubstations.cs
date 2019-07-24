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
    public class JobReadForeignSubstations
    {
        public async Task ImportForeignSubstations(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            SubstationExtract ssExtract = new SubstationExtract();
            List<SubstationForeign> ssForeignList = ssExtract.ExtractSubstationsForeign(oracleConnStr);

            LoadSubstation loadSS = new LoadSubstation();
            foreach (SubstationForeign ssForeign in ssForeignList)
            {
                Substation insertedSS = await loadSS.LoadSingleAsync(_context, _log, ssForeign, opt);
            }
        }
    }
}
