using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignRegions
    {
        public async Task ImportForeignRegions(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            RegionExtract regionExtract = new RegionExtract();
            List<Region> regions = regionExtract.ExtractRegions(oracleConnStr);

            LoadRegion loadRegion = new LoadRegion();
            foreach (Region region in regions)
            {
                Region insertedRegion = await loadRegion.LoadSingleAsync(_context, region, opt);
            }
        }
    }
}
