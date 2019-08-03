using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadHvdcLine
    {
        public async Task<HvdcLine> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, HvdcLineForeign hvdcLineForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            HvdcLine existingHvdcLine = await _context.HvdcLines.SingleOrDefaultAsync(lr => lr.WebUatId == hvdcLineForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingHvdcLine != null)
            {
                return existingHvdcLine;
            }

            // find the FromSubstation via the FromSSWebUatId
            int fromSSWebUatId = hvdcLineForeign.FromSSWebUatId;
            Substation fromSS = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == fromSSWebUatId);
            // if FromSubstation doesnot exist, skip the import. Ideally, there should not be such case
            if (fromSS == null)
            {
                _log.LogCritical($"Unable to find FromSubstation with webUatId {fromSSWebUatId} while inserting HvdcLine with webUatId {hvdcLineForeign.WebUatId} and name {hvdcLineForeign.Name}");
                return null;
            }

            // find the ToSubstation via the ToSSWebUatId
            int toSSWebUatId = hvdcLineForeign.ToSSWebUatId;
            Substation toSS = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == toSSWebUatId);
            // if ToSubstation doesnot exist, skip the import. Ideally, there should not be such case
            if (toSS == null)
            {
                _log.LogCritical($"Unable to find ToSubstation with webUatId {toSSWebUatId} while inserting HvdcLine with webUatId {hvdcLineForeign.WebUatId} and name {hvdcLineForeign.Name}");
                return null;
            }

            // find the FromState via the State WebUatId
            int fromStateWebUatId = hvdcLineForeign.FromStateWebUatId;
            State fromState = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == fromStateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (fromState == null)
            {
                _log.LogCritical($"Unable to find FromState with webUatId {fromStateWebUatId} while inserting HvdcLine with webUatId {hvdcLineForeign.WebUatId} and name {hvdcLineForeign.Name}");
                return null;
            }

            // find the ToState via the State WebUatId
            int toStateWebUatId = hvdcLineForeign.ToStateWebUatId;
            State toState = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == toStateWebUatId);
            // if toState doesnot exist, skip the import. Ideally, there should not be such case
            if (toState == null)
            {
                _log.LogCritical($"Unable to find ToState with webUatId {toStateWebUatId} while inserting HvdcLine with webUatId {hvdcLineForeign.WebUatId} and name {hvdcLineForeign.Name}");
                return null;
            }

            int voltLevelWebUatId = hvdcLineForeign.VoltLevelWebUatId;
            VoltLevel voltLevel = await _context.VoltLevels.SingleOrDefaultAsync(v => v.WebUatId == voltLevelWebUatId);
            // if voltLevel doesnot exist, skip the import. Ideally, there should not be such case
            if (voltLevel == null)
            {
                _log.LogCritical($"Unable to find VoltLevel with webUatId {voltLevelWebUatId} while inserting HvdcLine with webUatId {hvdcLineForeign.WebUatId} and name {hvdcLineForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingHvdcLine != null)
            {
                _context.HvdcLines.Remove(existingHvdcLine);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingHvdcLine == null || (opt == EntityWriteOption.Replace && existingHvdcLine != null))
            {
                HvdcLine newHvdcLine = new HvdcLine();
                newHvdcLine.Name = hvdcLineForeign.Name;
                newHvdcLine.FromStateId = fromState.StateId;
                newHvdcLine.ToStateId = toState.StateId;
                newHvdcLine.FromSubstationId = fromSS.SubstationId;
                newHvdcLine.ToSubstationId = toSS.SubstationId;
                newHvdcLine.VoltLevelId = voltLevel.VoltLevelId;
                newHvdcLine.WebUatId = hvdcLineForeign.WebUatId;
                _context.HvdcLines.Add(newHvdcLine);
                await _context.SaveChangesAsync();
                return newHvdcLine;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingHvdcLine != null)
            {
                existingHvdcLine.Name = hvdcLineForeign.Name;
                existingHvdcLine.FromStateId = fromState.StateId;
                existingHvdcLine.ToStateId = toState.StateId;
                existingHvdcLine.FromSubstationId = fromSS.SubstationId;
                existingHvdcLine.ToSubstationId = toSS.SubstationId;
                existingHvdcLine.VoltLevelId = voltLevel.VoltLevelId;
                await _context.SaveChangesAsync();
                return existingHvdcLine;
            }
            return null;
        }
    }
}