using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using System;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadSubstationOwner
    {
        public async Task<SubstationOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, SubstationOwnerForeign ssOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            SubstationOwner existingSSOwner = await _context.SubstationOwners.SingleOrDefaultAsync(ssO => ssO.WebUatId == ssOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingSSOwner != null)
            {
                return existingSSOwner;
            }

            // find the Substation via the Substation WebUatId
            int ssWebUatId = ssOwnerForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == ssWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {ssWebUatId} while inserting SubstationOwner with webUatId {ssOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the substation via the Owner WebUatId
            int ownerWebUatId = ssOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting SubstationOwner with webUatId {ssOwnerForeign.WebUatId}");
                return null;
            }

            try
            {
                // check if we have to replace the entity completely
                if (opt == EntityWriteOption.Replace && existingSSOwner != null)
                {
                    _context.SubstationOwners.Remove(existingSSOwner);
                }

                // if entity is not present, then insert or check if we have to replace the entity completely
                if (existingSSOwner == null || (opt == EntityWriteOption.Replace && existingSSOwner != null))
                {
                    SubstationOwner newSSOwner = new SubstationOwner();
                    newSSOwner.OwnerId = owner.OwnerId;
                    newSSOwner.SubstationId = substation.SubstationId;
                    newSSOwner.WebUatId = ssOwnerForeign.WebUatId;

                    _context.SubstationOwners.Add(newSSOwner);
                    await _context.SaveChangesAsync();
                    return newSSOwner;
                }

                // check if we have to modify the entity
                if (opt == EntityWriteOption.Modify && existingSSOwner != null)
                {
                    existingSSOwner.OwnerId = owner.OwnerId;
                    existingSSOwner.SubstationId = substation.SubstationId;
                    await _context.SaveChangesAsync();
                    return existingSSOwner;
                }
            }
            catch (DbUpdateException e)
            {
                _log.LogCritical($"Error occured while inserting SubstationOwner with webUatId {ssOwnerForeign.WebUatId}, owner id {owner.OwnerId} and ssId {substation.SubstationId}");
                _log.LogCritical($"EntityWriteOption = {opt.ToString()}");
                _log.LogCritical($"{e.Message}");
                return null;
            }

            return null;
        }
    }
}
