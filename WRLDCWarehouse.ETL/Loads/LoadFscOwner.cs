using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadFscOwner
    {
        public async Task<FscOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, FscOwnerForeign fscOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            FscOwner existingFscOwner = await _context.FscOwners.SingleOrDefaultAsync(fO => fO.WebUatId == fscOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingFscOwner != null)
            {
                return existingFscOwner;
            }

            // find the Fsc via the Fsc WebUatId
            int fscWebUatId = fscOwnerForeign.FscWebUatId;
            Fsc fsc = await _context.Fscs.SingleOrDefaultAsync(c => c.WebUatId == fscWebUatId);
            // if Fsc doesnot exist, skip the import. Ideally, there should not be such case
            if (fsc == null)
            {
                _log.LogCritical($"Unable to find Fsc with webUatId {fscWebUatId} while inserting FscOwner with webUatId {fscOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the Fsc via the Owner WebUatId
            int ownerWebUatId = fscOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting FscOwner with webUatId {fscOwnerForeign.WebUatId}");
                return null;
            }

            try
            {
                // check if we have to replace the entity completely
                if (opt == EntityWriteOption.Replace && existingFscOwner != null)
                {
                    _context.FscOwners.Remove(existingFscOwner);
                }

                // if entity is not present, then insert or check if we have to replace the entity completely
                if (existingFscOwner == null || (opt == EntityWriteOption.Replace && existingFscOwner != null))
                {
                    FscOwner newFscOwner = new FscOwner();
                    newFscOwner.OwnerId = owner.OwnerId;
                    newFscOwner.FscId = fsc.FscId;
                    newFscOwner.WebUatId = fscOwnerForeign.WebUatId;

                    _context.FscOwners.Add(newFscOwner);
                    await _context.SaveChangesAsync();
                    return newFscOwner;
                }

                // check if we have to modify the entity
                if (opt == EntityWriteOption.Modify && existingFscOwner != null)
                {
                    existingFscOwner.OwnerId = owner.OwnerId;
                    existingFscOwner.FscId = fsc.FscId;
                    await _context.SaveChangesAsync();
                    return existingFscOwner;
                }
            }
            catch (DbUpdateException e)
            {
                _log.LogCritical($"Error occured while inserting FscOwner with webUatId {fscOwnerForeign.WebUatId}, owner id {owner.OwnerId} and fscId {fsc.FscId}");
                _log.LogCritical($"EntityWriteOption = {opt.ToString()}");
                _log.LogCritical($"{e.Message}");
                return null;
            }

            return null;
        }
    }
}
