using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadMajorSubstation
    {
        public async Task<MajorSubstation> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, MajorSubstationForeign majorSSForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            MajorSubstation existingMajorSS = await _context.MajorSubstations.SingleOrDefaultAsync(mss => mss.WebUatId == majorSSForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingMajorSS != null)
            {
                return existingMajorSS;
            }

            // find the region of the state via the region WebUatId
            int stateWebUatId = majorSSForeign.StateWebUatId;
            State majorSSState = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);

            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (majorSSState == null)
            {
                _log.LogCritical($"Unable to find State with webUatId {stateWebUatId} while inserting MajorSubstation with webUatId {majorSSForeign.WebUatId} and name {majorSSForeign.Name}");
                return null;
            }

            // if entity is not present, then insert
            if (existingMajorSS == null)
            {
                MajorSubstation newMajorSS = new MajorSubstation();
                newMajorSS.Name = majorSSForeign.Name;
                newMajorSS.StateId = majorSSState.StateId;
                newMajorSS.WebUatId = majorSSForeign.WebUatId;

                _context.MajorSubstations.Add(newMajorSS);
                await _context.SaveChangesAsync();
                return newMajorSS;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingMajorSS != null)
            {
                _context.MajorSubstations.Remove(existingMajorSS);

                MajorSubstation newMajorSS = new MajorSubstation();
                newMajorSS.Name = majorSSForeign.Name;
                newMajorSS.StateId = majorSSState.StateId;
                newMajorSS.WebUatId = majorSSForeign.WebUatId;

                _context.MajorSubstations.Add(newMajorSS);
                await _context.SaveChangesAsync();
                return newMajorSS;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingMajorSS != null)
            {
                existingMajorSS.Name = majorSSForeign.Name;
                existingMajorSS.StateId = majorSSState.StateId;
                await _context.SaveChangesAsync();
                return existingMajorSS;
            }
            return null;
        }
    }
}
