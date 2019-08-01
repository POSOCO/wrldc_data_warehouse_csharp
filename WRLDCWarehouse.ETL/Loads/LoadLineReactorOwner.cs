using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadLineReactorOwner
    {
        public async Task<LineReactorOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, LineReactorOwnerForeign lrOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            LineReactorOwner existingLrOwner = await _context.LineReactorOwners.SingleOrDefaultAsync(lrO => lrO.WebUatId == lrOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingLrOwner != null)
            {
                return existingLrOwner;
            }

            // find the BusReactor via the BusReactorWebUatId
            int lrWebUatId = lrOwnerForeign.LineReactorWebUatId;
            LineReactor lineReactor = await _context.LineReactors.SingleOrDefaultAsync(lr => lr.WebUatId == lrWebUatId);
            // if LineReactor doesnot exist, skip the import. Ideally, there should not be such case
            if (lineReactor == null)
            {
                _log.LogCritical($"Unable to find LineReactor with webUatId {lrWebUatId} while inserting LineReactorOwner with webUatId {lrOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the substation via the Owner WebUatId
            int ownerWebUatId = lrOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting LineReactorOwner with webUatId {lrOwnerForeign.WebUatId}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingLrOwner != null)
            {
                _context.LineReactorOwners.Remove(existingLrOwner);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingLrOwner == null || (opt == EntityWriteOption.Replace && existingLrOwner != null))
            {
                LineReactorOwner newLrOwner = new LineReactorOwner();
                newLrOwner.OwnerId = owner.OwnerId;
                newLrOwner.LineReactorId = lineReactor.LineReactorId;
                newLrOwner.WebUatId = lrOwnerForeign.WebUatId;

                _context.LineReactorOwners.Add(newLrOwner);
                await _context.SaveChangesAsync();
                return newLrOwner;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingLrOwner != null)
            {
                existingLrOwner.OwnerId = owner.OwnerId;
                existingLrOwner.LineReactorId = lineReactor.LineReactorId;
                await _context.SaveChangesAsync();
                return existingLrOwner;
            }
            return null;
        }
    }
}
