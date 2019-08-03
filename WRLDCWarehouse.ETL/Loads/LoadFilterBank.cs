using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadFilterBank
    {
        public async Task<FilterBank> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, FilterBankForeign fbForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            FilterBank existingFb = await _context.FilterBanks.SingleOrDefaultAsync(lr => lr.WebUatId == fbForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingFb != null)
            {
                return existingFb;
            }

            // find the region
            int regionWebUatId = fbForeign.RegionWebUatId;
            Region region = await _context.Regions.SingleOrDefaultAsync(r => r.WebUatId == regionWebUatId);
            // if region doesnot exist, skip the import. Ideally, there should not be such case
            if (region == null)
            {
                _log.LogCritical($"Unable to find region with webUatId {regionWebUatId} while inserting FilterBank with webUatId {fbForeign.WebUatId} and name {fbForeign.Name}");
                return null;
            }

            // find the State of the LineReactor via the State WebUatId
            int stateWebUatId = fbForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                _log.LogCritical($"Unable to find State with webUatId {stateWebUatId} while inserting FilterBank with webUatId {fbForeign.WebUatId} and name {fbForeign.Name}");
                return null;
            }

            // find the Substation of the LineReactor via the Substation WebUatId
            int ssWebUatId = fbForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == ssWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {ssWebUatId} while inserting FilterBank with webUatId {fbForeign.WebUatId} and name {fbForeign.Name}");
                return null;
            }

            int voltLevelWebUatId = fbForeign.VoltLevelWebUatId;
            VoltLevel voltLevel = await _context.VoltLevels.SingleOrDefaultAsync(v => v.WebUatId == voltLevelWebUatId);
            // if voltLevel doesnot exist, skip the import. Ideally, there should not be such case
            if (voltLevel == null)
            {
                _log.LogCritical($"Unable to find VoltLevel with webUatId {voltLevelWebUatId} while inserting FilterBank with webUatId {fbForeign.WebUatId} and name {fbForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingFb != null)
            {
                _context.FilterBanks.Remove(existingFb);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingFb == null || (opt == EntityWriteOption.Replace && existingFb != null))
            {
                FilterBank newFb = new FilterBank();
                newFb.Name = fbForeign.Name;
                newFb.RegionId = region.RegionId;
                newFb.StateId = state.StateId;
                newFb.SubstationId = substation.SubstationId;
                newFb.VoltLevelId = voltLevel.VoltLevelId;
                newFb.IsSwitchable = fbForeign.IsSwitchable == 0 ? false : true;
                newFb.FilterBankNumber = fbForeign.FilterBankNumber.ToString();

                newFb.WebUatId = fbForeign.WebUatId;
                _context.FilterBanks.Add(newFb);
                await _context.SaveChangesAsync();
                return newFb;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingFb != null)
            {
                existingFb.Name = fbForeign.Name;
                existingFb.RegionId = region.RegionId;
                existingFb.StateId = state.StateId;
                existingFb.SubstationId = substation.SubstationId;
                existingFb.VoltLevelId = voltLevel.VoltLevelId;
                existingFb.IsSwitchable = fbForeign.IsSwitchable == 0 ? false : true;
                existingFb.FilterBankNumber = fbForeign.FilterBankNumber.ToString();
                await _context.SaveChangesAsync();
                return existingFb;
            }
            return null;
        }
    }
}