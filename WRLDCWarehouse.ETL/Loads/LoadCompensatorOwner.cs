using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadCompensatorOwner
    {
        public async Task<CompensatorOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, CompensatorOwnerForeign compensatorOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            CompensatorOwner existingCompensatorOwner = await _context.CompensatorOwners.SingleOrDefaultAsync(cO => cO.WebUatId == compensatorOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingCompensatorOwner != null)
            {
                return existingCompensatorOwner;
            }

            // find the Compensator via the Compensator WebUatId
            int compensatorWebUatId = compensatorOwnerForeign.CompensatorWebUatId;
            Compensator compensator = await _context.Compensators.SingleOrDefaultAsync(c => c.WebUatId == compensatorWebUatId);
            // if Compensator doesnot exist, skip the import. Ideally, there should not be such case
            if (compensator == null)
            {
                _log.LogCritical($"Unable to find Compensator with webUatId {compensatorWebUatId} while inserting CompensatorOwner with webUatId {compensatorOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the Compensator via the Owner WebUatId
            int ownerWebUatId = compensatorOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting CompensatorOwner with webUatId {compensatorOwnerForeign.WebUatId}");
                return null;
            }

            try
            {
                // check if we have to replace the entity completely
                if (opt == EntityWriteOption.Replace && existingCompensatorOwner != null)
                {
                    _context.CompensatorOwners.Remove(existingCompensatorOwner);
                }

                // if entity is not present, then insert or check if we have to replace the entity completely
                if (existingCompensatorOwner == null || (opt == EntityWriteOption.Replace && existingCompensatorOwner != null))
                {
                    CompensatorOwner newCompensatorOwner = new CompensatorOwner();
                    newCompensatorOwner.OwnerId = owner.OwnerId;
                    newCompensatorOwner.CompensatorId = compensator.CompensatorId;
                    newCompensatorOwner.WebUatId = compensatorOwnerForeign.WebUatId;

                    _context.CompensatorOwners.Add(newCompensatorOwner);
                    await _context.SaveChangesAsync();
                    return newCompensatorOwner;
                }

                // check if we have to modify the entity
                if (opt == EntityWriteOption.Modify && existingCompensatorOwner != null)
                {
                    existingCompensatorOwner.OwnerId = owner.OwnerId;
                    existingCompensatorOwner.CompensatorId = compensator.CompensatorId;
                    await _context.SaveChangesAsync();
                    return existingCompensatorOwner;
                }
            }
            catch (DbUpdateException e)
            {
                _log.LogCritical($"Error occured while inserting CompensatorOwner with webUatId {compensatorOwnerForeign.WebUatId}, owner id {owner.OwnerId} and compensatorId {compensator.CompensatorId}");
                _log.LogCritical($"EntityWriteOption = {opt.ToString()}");
                _log.LogCritical($"{e.Message}");
                return null;
            }

            return null;
        }
    }
}
