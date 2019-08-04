using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadHvdcPole
    {
        public async Task<HvdcPole> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, HvdcPoleForeign hvdcPoleForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            HvdcPole existingHvdcPole = await _context.HvdcPoles.SingleOrDefaultAsync(lr => lr.WebUatId == hvdcPoleForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingHvdcPole != null)
            {
                return existingHvdcPole;
            }

            // find the Substation via the SubstationWebUatId
            int substationWebUatId = hvdcPoleForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == substationWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {substationWebUatId} while inserting HvdcPole with webUatId {hvdcPoleForeign.WebUatId} and name {hvdcPoleForeign.Name}");
                return null;
            }

            // find the State via the State WebUatId
            int stateWebUatId = hvdcPoleForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                _log.LogCritical($"Unable to find state with webUatId {stateWebUatId} while inserting HvdcPole with webUatId {hvdcPoleForeign.WebUatId} and name {hvdcPoleForeign.Name}");
                return null;
            }

            int voltLevelWebUatId = hvdcPoleForeign.VoltLevelWebUatId;
            VoltLevel voltLevel = await _context.VoltLevels.SingleOrDefaultAsync(v => v.WebUatId == voltLevelWebUatId);
            // if voltLevel doesnot exist, skip the import. Ideally, there should not be such case
            if (voltLevel == null)
            {
                _log.LogCritical($"Unable to find VoltLevel with webUatId {voltLevelWebUatId} while inserting HvdcPole with webUatId {hvdcPoleForeign.WebUatId} and name {hvdcPoleForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingHvdcPole != null)
            {
                _context.HvdcPoles.Remove(existingHvdcPole);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingHvdcPole == null || (opt == EntityWriteOption.Replace && existingHvdcPole != null))
            {
                HvdcPole newHvdcPole = new HvdcPole();
                newHvdcPole.Name = hvdcPoleForeign.Name;
                newHvdcPole.PoleNumber = hvdcPoleForeign.PoleNumber.ToString();
                newHvdcPole.SubstationId = substation.SubstationId;
                newHvdcPole.StateId = state.StateId;
                newHvdcPole.VoltLevelId = voltLevel.VoltLevelId;
                newHvdcPole.PoleType = hvdcPoleForeign.PoleType;
                newHvdcPole.MaxFiringAngleDegrees = hvdcPoleForeign.MaxFiringAngleDegrees;
                newHvdcPole.MinFiringAngleDegrees = hvdcPoleForeign.MinFiringAngleDegrees;
                newHvdcPole.ThermalLimitMVA = hvdcPoleForeign.ThermalLimitMVA;
                newHvdcPole.CommDate = hvdcPoleForeign.CommDate;
                newHvdcPole.CodDate = hvdcPoleForeign.CodDate;
                newHvdcPole.DeCommDate = hvdcPoleForeign.DeCommDate;
                newHvdcPole.WebUatId = hvdcPoleForeign.WebUatId;
                _context.HvdcPoles.Add(newHvdcPole);
                await _context.SaveChangesAsync();
                return newHvdcPole;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingHvdcPole != null)
            {
                existingHvdcPole.Name = hvdcPoleForeign.Name;
                existingHvdcPole.PoleNumber = hvdcPoleForeign.PoleNumber.ToString();
                existingHvdcPole.SubstationId = substation.SubstationId;
                existingHvdcPole.StateId = state.StateId;
                existingHvdcPole.VoltLevelId = voltLevel.VoltLevelId;
                existingHvdcPole.PoleType = hvdcPoleForeign.PoleType;
                existingHvdcPole.MaxFiringAngleDegrees = hvdcPoleForeign.MaxFiringAngleDegrees;
                existingHvdcPole.MinFiringAngleDegrees = hvdcPoleForeign.MinFiringAngleDegrees;
                existingHvdcPole.ThermalLimitMVA = hvdcPoleForeign.ThermalLimitMVA;
                existingHvdcPole.CommDate = hvdcPoleForeign.CommDate;
                existingHvdcPole.CodDate = hvdcPoleForeign.CodDate;
                existingHvdcPole.DeCommDate = hvdcPoleForeign.DeCommDate;
                await _context.SaveChangesAsync();
                return existingHvdcPole;
            }
            return null;
        }
    }
}