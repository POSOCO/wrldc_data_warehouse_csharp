using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadBayOwner
    {
        public async Task<BayOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, BayOwnerForeign bayOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            BayOwner existingBayOwner = await _context.BayOwners.SingleOrDefaultAsync(brO => brO.WebUatId == bayOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingBayOwner != null)
            {
                return existingBayOwner;
            }

            // find the bay via the BayWebUatId
            int bayWebUatId = bayOwnerForeign.BayWebUatId;
            Bay bay = await _context.Bays.SingleOrDefaultAsync(b => b.WebUatId == bayWebUatId);
            // if Bay doesnot exist, skip the import. Ideally, there should not be such case
            if (bay == null)
            {
                _log.LogCritical($"Unable to find Bay with webUatId {bayWebUatId} while inserting BayOwner with webUatId {bayOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the Bay via the Owner WebUatId
            int ownerWebUatId = bayOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting BayOwner with webUatId {bayOwnerForeign.WebUatId}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingBayOwner != null)
            {
                _context.BayOwners.Remove(existingBayOwner);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingBayOwner == null || (opt == EntityWriteOption.Replace && existingBayOwner != null))
            {
                BayOwner newBayOwner = new BayOwner();
                newBayOwner.OwnerId = owner.OwnerId;
                newBayOwner.BayId = bay.BayId;
                newBayOwner.WebUatId = bayOwnerForeign.WebUatId;

                _context.BayOwners.Add(newBayOwner);
                await _context.SaveChangesAsync();
                return newBayOwner;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingBayOwner != null)
            {
                existingBayOwner.OwnerId = owner.OwnerId;
                existingBayOwner.BayId = bay.BayId;
                await _context.SaveChangesAsync();
                return existingBayOwner;
            }
            return null;
        }
    }
}
