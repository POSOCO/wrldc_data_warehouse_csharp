using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadBusReactorOwner
    {
        public async Task<BusReactorOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, BusReactorOwnerForeign brOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            BusReactorOwner existingBrOwner = await _context.BusReactorOwners.SingleOrDefaultAsync(brO => brO.WebUatId == brOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingBrOwner != null)
            {
                return existingBrOwner;
            }

            // find the BusReactor via the BusReactorWebUatId
            int brWebUatId = brOwnerForeign.BusReactorWebUatId;
            BusReactor busReactor = await _context.BusReactors.SingleOrDefaultAsync(br => br.WebUatId == brWebUatId);
            // if BusReactor doesnot exist, skip the import. Ideally, there should not be such case
            if (busReactor == null)
            {
                _log.LogCritical($"Unable to find BusReactor with webUatId {brWebUatId} while inserting BusReactorOwner with webUatId {brOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the substation via the Owner WebUatId
            int ownerWebUatId = brOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting BusReactorOwner with webUatId {brOwnerForeign.WebUatId}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingBrOwner != null)
            {
                _context.BusReactorOwners.Remove(existingBrOwner);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingBrOwner == null || (opt == EntityWriteOption.Replace && existingBrOwner != null))
            {
                BusReactorOwner newBrOwner = new BusReactorOwner();
                newBrOwner.OwnerId = owner.OwnerId;
                newBrOwner.BusReactorId = busReactor.BusReactorId;
                newBrOwner.WebUatId = brOwnerForeign.WebUatId;

                _context.BusReactorOwners.Add(newBrOwner);
                await _context.SaveChangesAsync();
                return newBrOwner;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingBrOwner != null)
            {
                existingBrOwner.OwnerId = owner.OwnerId;
                existingBrOwner.BusReactorId = busReactor.BusReactorId;
                await _context.SaveChangesAsync();
                return existingBrOwner;
            }
            return null;
        }
    }
}
