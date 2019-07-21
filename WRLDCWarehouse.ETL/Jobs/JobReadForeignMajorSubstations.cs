using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignMajorSubstations
    {
        public async Task ImportForeignMajorSubstations(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            MajorSubstationExtract majorSSExtract = new MajorSubstationExtract();
            List<MajorSubstationForeign> majorSSForeigns = majorSSExtract.ExtractMajorSubstationsForeign(oracleConnStr);

            LoadMajorSubstation loadMajorSS = new LoadMajorSubstation();
            foreach (MajorSubstationForeign majorSSForeign in majorSSForeigns)
            {
                MajorSubstation insertedMajorSS = await loadMajorSS.LoadSingleAsync(_context, majorSSForeign, opt);
            }
        }
    }
}
