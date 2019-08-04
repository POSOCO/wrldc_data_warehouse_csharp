using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadHvdcPoleOwner
    {
        public async Task<HvdcPoleOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, HvdcPoleOwnerForeign poleOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            HvdcPoleOwner existingPoleOwner = await _context.HvdcPoleOwners.SingleOrDefaultAsync(cO => cO.WebUatId == poleOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingPoleOwner != null)
            {
                return existingPoleOwner;
            }

            // find the HvdcPole via the HvdcPole WebUatId
            int poleWebUatId = poleOwnerForeign.HvdcPoleWebUatId;
            HvdcPole pole = await _context.HvdcPoles.SingleOrDefaultAsync(c => c.WebUatId == poleWebUatId);
            // if HvdcPole doesnot exist, skip the import. Ideally, there should not be such case
            if (pole == null)
            {
                _log.LogCritical($"Unable to find HvdcPole with webUatId {poleWebUatId} while inserting HvdcPoleOwner with webUatId {poleOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the Pole via the Owner WebUatId
            int ownerWebUatId = poleOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting HvdcPoleOwner with webUatId {poleOwnerForeign.WebUatId}");
                return null;
            }

            try
            {
                // check if we have to replace the entity completely
                if (opt == EntityWriteOption.Replace && existingPoleOwner != null)
                {
                    _context.HvdcPoleOwners.Remove(existingPoleOwner);
                }

                // if entity is not present, then insert or check if we have to replace the entity completely
                if (existingPoleOwner == null || (opt == EntityWriteOption.Replace && existingPoleOwner != null))
                {
                    HvdcPoleOwner newPoleOwner = new HvdcPoleOwner();
                    newPoleOwner.OwnerId = owner.OwnerId;
                    newPoleOwner.HvdcPoleId = pole.HvdcPoleId;
                    newPoleOwner.WebUatId = poleOwnerForeign.WebUatId;

                    _context.HvdcPoleOwners.Add(newPoleOwner);
                    await _context.SaveChangesAsync();
                    return newPoleOwner;
                }

                // check if we have to modify the entity
                if (opt == EntityWriteOption.Modify && existingPoleOwner != null)
                {
                    existingPoleOwner.OwnerId = owner.OwnerId;
                    existingPoleOwner.HvdcPoleId = pole.HvdcPoleId;
                    await _context.SaveChangesAsync();
                    return existingPoleOwner;
                }
            }
            catch (DbUpdateException e)
            {
                _log.LogCritical($"Error occured while inserting HvdcPoleOwner with webUatId {poleOwnerForeign.WebUatId}, owner id {owner.OwnerId} and poleId {pole.HvdcPoleId}");
                _log.LogCritical($"EntityWriteOption = {opt.ToString()}");
                _log.LogCritical($"{e.Message}");
                return null;
            }

            return null;
        }
    }
}
