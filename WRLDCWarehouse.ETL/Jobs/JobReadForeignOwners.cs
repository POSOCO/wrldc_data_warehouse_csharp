using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignOwners
    {
        public async Task ImportForeignOwners(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            OwnerExtract ownerExtract = new OwnerExtract();
            List<Owner> owners = ownerExtract.ExtractOwners(oracleConnStr);

            LoadOwner loadOwner = new LoadOwner();
            foreach (Owner owner in owners)
            {
                Owner insertedOwner = await loadOwner.LoadSingleAsync(_context, owner, opt);
            }
        }
    }
}
