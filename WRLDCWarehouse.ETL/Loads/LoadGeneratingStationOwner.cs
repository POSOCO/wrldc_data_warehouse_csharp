using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadGeneratingStationOwner
    {
        public async Task<GeneratingStationOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, GeneratingStationOwnerForeign genStationOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            GeneratingStationOwner existingGenStationOwner = await _context.GeneratingStationOwners.SingleOrDefaultAsync(gso => gso.WebUatId == genStationOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingGenStationOwner != null)
            {
                return existingGenStationOwner;
            }

            // find the GeneratingStation via the GeneratingStation WebUatId
            int genStationWebUatId = genStationOwnerForeign.GeneratingStationWebUatId;
            GeneratingStation genStation = await _context.GeneratingStations.SingleOrDefaultAsync(gs => gs.WebUatId == genStationWebUatId);
            // if GeneratingStation doesnot exist, skip the import. Ideally, there should not be such case
            if (genStation == null)
            {
                _log.LogCritical($"Unable to find GeneratingStation with webUatId {genStationWebUatId} while inserting GeneratingStationOwner with webUatId {genStationOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the substation via the Owner WebUatId
            int ownerWebUatId = genStationOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting GeneratingStationOwner with webUatId {genStationOwnerForeign.WebUatId}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingGenStationOwner != null)
            {
                _context.GeneratingStationOwners.Remove(existingGenStationOwner);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingGenStationOwner == null || (opt == EntityWriteOption.Replace && existingGenStationOwner != null))
            {
                GeneratingStationOwner genStationOwner = new GeneratingStationOwner();
                genStationOwner.OwnerId = owner.OwnerId;
                genStationOwner.GeneratingStationId = genStation.GeneratingStationId;
                genStationOwner.WebUatId = genStationOwnerForeign.WebUatId;

                _context.GeneratingStationOwners.Add(genStationOwner);
                await _context.SaveChangesAsync();
                return genStationOwner;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingGenStationOwner != null)
            {
                existingGenStationOwner.OwnerId = owner.OwnerId;
                existingGenStationOwner.GeneratingStationId = genStation.GeneratingStationId;
                await _context.SaveChangesAsync();
                return existingGenStationOwner;
            }
            return null;
        }
    }
}
