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
    public class JobReadForeignSubstationOwners
    {
        public async Task ImportForeignSubstationOwners(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            SubstationOwnerExtract ssOwnerExtract = new SubstationOwnerExtract();
            List<SubstationOwnerForeign> ssOwnersForeign = ssOwnerExtract.ExtractSubstationOwnersForeign(oracleConnStr);

            LoadSubstationOwner loadSSOwner = new LoadSubstationOwner();
            foreach (SubstationOwnerForeign ssOwnerForeign in ssOwnersForeign)
            {
                SubstationOwner insertedSSOwner = await loadSSOwner.LoadSingleAsync(_context, ssOwnerForeign, opt);
            }
        }
    }
}
