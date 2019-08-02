using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadFilterBankOwner
    {
        public async Task<FilterBankOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, FilterBankOwnerForeign fbOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            FilterBankOwner existingFbOwner = await _context.FilterBankOwners.SingleOrDefaultAsync(fbO => fbO.WebUatId == fbOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingFbOwner != null)
            {
                return existingFbOwner;
            }

            // find the FilterBank via the FilterBank WebUatId
            int fbWebUatId = fbOwnerForeign.FilterBankWebUatId;
            FilterBank filterBank = await _context.FilterBanks.SingleOrDefaultAsync(ss => ss.WebUatId == fbWebUatId);
            // if FilterBank doesnot exist, skip the import. Ideally, there should not be such case
            if (filterBank == null)
            {
                _log.LogCritical($"Unable to find FilterBank with webUatId {fbWebUatId} while inserting FilterBankOwner with webUatId {fbOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the substation via the Owner WebUatId
            int ownerWebUatId = fbOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find FilterBank with webUatId {ownerWebUatId} while inserting FilterBankOwner with webUatId {fbOwnerForeign.WebUatId}");
                return null;
            }

            try
            {
                // check if we have to replace the entity completely
                if (opt == EntityWriteOption.Replace && existingFbOwner != null)
                {
                    _context.FilterBankOwners.Remove(existingFbOwner);
                }

                // if entity is not present, then insert or check if we have to replace the entity completely
                if (existingFbOwner == null || (opt == EntityWriteOption.Replace && existingFbOwner != null))
                {
                    FilterBankOwner newFbOwner = new FilterBankOwner();
                    newFbOwner.OwnerId = owner.OwnerId;
                    newFbOwner.FilterBankId = filterBank.FilterBankId;
                    newFbOwner.WebUatId = fbOwnerForeign.WebUatId;

                    _context.FilterBankOwners.Add(newFbOwner);
                    await _context.SaveChangesAsync();
                    return newFbOwner;
                }

                // check if we have to modify the entity
                if (opt == EntityWriteOption.Modify && existingFbOwner != null)
                {
                    existingFbOwner.OwnerId = owner.OwnerId;
                    existingFbOwner.FilterBankId = filterBank.FilterBankId;
                    await _context.SaveChangesAsync();
                    return existingFbOwner;
                }
            }
            catch (DbUpdateException e)
            {
                _log.LogCritical($"Error occured while inserting FilterBankOwner with webUatId {fbOwnerForeign.WebUatId}, owner id {owner.OwnerId} and ssId {filterBank.SubstationId}");
                _log.LogCritical($"EntityWriteOption = {opt.ToString()}");
                _log.LogCritical($"{e.Message}");
                return null;
            }

            return null;
        }
    }
}
