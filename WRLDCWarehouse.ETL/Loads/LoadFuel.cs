using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadFuel
    {
        public async Task<Fuel> LoadSingleAsync(WRLDCWarehouseDbContext _context, Fuel fuel, EntityWriteOption opt)
        {
            // check if entity already exists
            Fuel existingFuel = await _context.Fuels.SingleOrDefaultAsync(r => r.WebUatId == fuel.WebUatId);

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingFuel != null)
            {
                _context.Fuels.Remove(existingFuel);
            }

            // check if entity is not present or check if we have to replace the entity completely
            if (existingFuel == null || (opt == EntityWriteOption.Replace && existingFuel != null))
            {
                _context.Fuels.Add(fuel);
                await _context.SaveChangesAsync();
                return fuel;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingFuel != null)
            {
                return existingFuel;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingFuel != null)
            {
                existingFuel.Name = fuel.Name;
                await _context.SaveChangesAsync();
                return existingFuel;
            }
            return null;
        }
    }
}
