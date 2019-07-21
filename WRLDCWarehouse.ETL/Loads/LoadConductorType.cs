using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadConductorType
    {
        public async Task<ConductorType> LoadSingleAsync(WRLDCWarehouseDbContext _context, ConductorType condType, EntityWriteOption opt)
        {
            // check if frequencySummary already exists for the date and delete it
            ConductorType existingCondType = await _context.ConductorTypes.SingleOrDefaultAsync(ct => ct.WebUatId == condType.WebUatId);

            // if entity is not present, then insert
            if (existingCondType == null)
            {
                _context.ConductorTypes.Add(condType);
                await _context.SaveChangesAsync();
                return condType;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingCondType != null)
            {
                return existingCondType;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingCondType != null)
            {
                _context.ConductorTypes.Remove(existingCondType);
                _context.ConductorTypes.Add(condType);
                await _context.SaveChangesAsync();
                return condType;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingCondType != null)
            {
                existingCondType.Name = condType.Name;
                await _context.SaveChangesAsync();
                return existingCondType;
            }
            return null;
        }
    }
}
