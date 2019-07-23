using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadGenerationType
    {
        public async Task<GenerationType> LoadSingleAsync(WRLDCWarehouseDbContext _context, GenerationType genType, EntityWriteOption opt)
        {
            // check if entity already exists
            GenerationType existingGenType = await _context.GenerationTypes.SingleOrDefaultAsync(gt => gt.WebUatId == genType.WebUatId);

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingGenType != null)
            {
                _context.GenerationTypes.Remove(existingGenType);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingGenType == null || (opt == EntityWriteOption.Replace && existingGenType != null))
            {
                _context.GenerationTypes.Add(genType);
                await _context.SaveChangesAsync();
                return genType;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingGenType != null)
            {
                return existingGenType;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingGenType != null)
            {
                existingGenType.Name = genType.Name;
                await _context.SaveChangesAsync();
                return existingGenType;
            }
            return null;
        }
    }
}
