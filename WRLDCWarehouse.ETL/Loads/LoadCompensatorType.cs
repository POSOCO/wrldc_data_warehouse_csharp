using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadCompensatorType
    {
        public async Task<CompensatorType> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, CompensatorType compensatorType, EntityWriteOption opt)
        {
            // check if entity already exists
            CompensatorType existingCompensatorType = await _context.CompensatorTypes.SingleOrDefaultAsync(r => r.WebUatId == compensatorType.WebUatId);

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingCompensatorType != null)
            {
                _context.CompensatorTypes.Remove(existingCompensatorType);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingCompensatorType == null || (opt == EntityWriteOption.Replace && existingCompensatorType != null))
            {
                _context.CompensatorTypes.Add(compensatorType);
                await _context.SaveChangesAsync();
                return compensatorType;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingCompensatorType != null)
            {
                return existingCompensatorType;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingCompensatorType != null)
            {
                existingCompensatorType.Name = compensatorType.Name;
                await _context.SaveChangesAsync();
                return existingCompensatorType;
            }
            return null;
        }
    }
}