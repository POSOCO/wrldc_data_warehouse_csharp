using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadState
    {
        public async Task<State> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, StateForeign stateForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            State existingState = await _context.States.SingleOrDefaultAsync(r => r.WebUatId == stateForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingState != null)
            {
                return existingState;
            }

            // find the region of the state via the region WebUatId
            int regionWebUatId = stateForeign.RegionWebUatId;
            Region stateRegion = await _context.Regions.SingleOrDefaultAsync(r => r.WebUatId == regionWebUatId);

            // if region doesnot exist, skip the import. Ideally, there should not be such case
            if (stateRegion == null)
            {
                _log.LogCritical($"Unable to find region with webUatId {regionWebUatId} while inserting state with webUatId {stateForeign.WebUatId} and name {stateForeign.FullName}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingState != null)
            {
                _context.States.Remove(existingState);
            }

            // if region is not present, then insert or check if we have to replace the entity completely
            if (existingState == null || (opt == EntityWriteOption.Replace && existingState != null))
            {
                State newState = new State();
                newState.FullName = stateForeign.FullName;
                newState.ShortName = stateForeign.ShortName;
                newState.RegionId = stateRegion.RegionId;
                newState.WebUatId = stateForeign.WebUatId;

                _context.States.Add(newState);
                await _context.SaveChangesAsync();
                return newState;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingState != null)
            {
                existingState.FullName = stateForeign.FullName;
                existingState.ShortName = stateForeign.ShortName;
                existingState.RegionId = stateRegion.RegionId;
                await _context.SaveChangesAsync();
                return existingState;
            }
            return null;
        }
    }
}
