using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadRegion
    {
        public async Task<Region> LoadSingleAsync(WRLDCWarehouseDbContext _context, Region region, EntityWriteOption opt)
        {
            // check if entity already exists
            Region existingRegion = await _context.Regions.SingleOrDefaultAsync(r => r.WebUatId == region.WebUatId);

            // if region is not present, then insert
            if (existingRegion == null)
            {
                _context.Regions.Add(region);
                await _context.SaveChangesAsync();
                return region;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingRegion != null)
            {
                return existingRegion;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingRegion != null)
            {
                _context.Regions.Remove(existingRegion);
                _context.Regions.Add(region);
                await _context.SaveChangesAsync();
                return region;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingRegion != null)
            {
                existingRegion.FullName = region.FullName;
                existingRegion.ShortName = region.ShortName;
                await _context.SaveChangesAsync();
                return existingRegion;
            }
            return null;
        }
    }
}
