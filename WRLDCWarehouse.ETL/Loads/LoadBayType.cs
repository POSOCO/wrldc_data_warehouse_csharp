using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadBayType
    {
        public async Task<BayType> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, BayType bayType, EntityWriteOption opt)
        {
            // check if entity already exists
            BayType existingBayType = await _context.BayTypes.SingleOrDefaultAsync(bt => bt.WebUatId == bayType.WebUatId);

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingBayType != null)
            {
                _context.BayTypes.Remove(existingBayType);
            }

            // check if entity is not present or check if we have to replace the entity completely
            if (existingBayType == null || (opt == EntityWriteOption.Replace && existingBayType != null))
            {
                _context.BayTypes.Add(bayType);
                await _context.SaveChangesAsync();
                return bayType;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingBayType != null)
            {
                return existingBayType;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingBayType != null)
            {
                existingBayType.Name = bayType.Name;
                await _context.SaveChangesAsync();
                return existingBayType;
            }
            return null;
        }
    }
}
