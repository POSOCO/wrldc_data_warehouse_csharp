using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadOwner
    {
        public async Task<Owner> LoadSingleAsync(WRLDCWarehouseDbContext _context, Owner owner, EntityWriteOption opt)
        {
            // check if entity already exists
            Owner existingOwner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == owner.WebUatId);

            // if region is not present, then insert
            if (existingOwner == null)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return owner;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingOwner != null)
            {
                return existingOwner;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingOwner != null)
            {
                _context.Owners.Remove(existingOwner);
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return owner;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingOwner != null)
            {
                existingOwner.Name = owner.Name;
                await _context.SaveChangesAsync();
                return existingOwner;
            }
            return null;
        }
    }
}
