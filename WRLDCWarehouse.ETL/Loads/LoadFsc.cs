using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadFsc
    {
        public async Task<Fsc> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, FscForeign fscForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            Fsc existingFsc = await _context.Fscs.SingleOrDefaultAsync(lr => lr.WebUatId == fscForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingFsc != null)
            {
                return existingFsc;
            }

            // find the Substation via the SubstationWebUatId
            int substationWebUatId = fscForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == substationWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {substationWebUatId} while inserting Fsc with webUatId {fscForeign.WebUatId} and name {fscForeign.Name}");
                return null;
            }

            // find the State via the State WebUatId
            int stateWebUatId = fscForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                _log.LogCritical($"Unable to find state with webUatId {stateWebUatId} while inserting Fsc with webUatId {fscForeign.WebUatId} and name {fscForeign.Name}");
                return null;
            }

            int cktWebUatId = fscForeign.AcTransLineCktWebUatId;
            AcTransLineCkt ckt = await _context.AcTransLineCkts.SingleOrDefaultAsync(v => v.WebUatId == cktWebUatId);
            // if ckt doesnot exist, skip the import. Ideally, there should not be such case
            if (ckt == null)
            {
                _log.LogCritical($"Unable to find AcTransLineCkt with webUatId {cktWebUatId} while inserting Fsc with webUatId {fscForeign.WebUatId} and name {fscForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingFsc != null)
            {
                _context.Fscs.Remove(existingFsc);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingFsc == null || (opt == EntityWriteOption.Replace && existingFsc != null))
            {
                Fsc newFsc = new Fsc();
                newFsc.Name = fscForeign.Name;
                newFsc.AcTransLineCktId = ckt.AcTransLineCktId;
                newFsc.SubstationId = substation.SubstationId;
                newFsc.StateId = state.StateId;
                newFsc.PercComp = fscForeign.PercComp;
                newFsc.LineReactance = fscForeign.LineReactance;
                newFsc.CapacitiveReactance = fscForeign.CapacitiveReactance;
                newFsc.RatedMvarPhase = fscForeign.RatedMvarPhase;
                newFsc.RatedCurrentAmps = fscForeign.RatedCurrentAmps;
                newFsc.CommDate = fscForeign.CommDate;
                newFsc.CodDate = fscForeign.CodDate;
                newFsc.DeCommDate = fscForeign.DeCommDate;
                newFsc.WebUatId = fscForeign.WebUatId;
                _context.Fscs.Add(newFsc);
                await _context.SaveChangesAsync();
                return newFsc;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingFsc != null)
            {
                existingFsc.Name = fscForeign.Name;
                existingFsc.AcTransLineCktId = ckt.AcTransLineCktId;
                existingFsc.SubstationId = substation.SubstationId;
                existingFsc.StateId = state.StateId;
                existingFsc.PercComp = fscForeign.PercComp;
                existingFsc.LineReactance = fscForeign.LineReactance;
                existingFsc.CapacitiveReactance = fscForeign.CapacitiveReactance;
                existingFsc.RatedMvarPhase = fscForeign.RatedMvarPhase;
                existingFsc.RatedCurrentAmps = fscForeign.RatedCurrentAmps;
                existingFsc.CommDate = fscForeign.CommDate;
                existingFsc.CodDate = fscForeign.CodDate;
                existingFsc.DeCommDate = fscForeign.DeCommDate;
                await _context.SaveChangesAsync();
                return existingFsc;
            }
            return null;
        }
    }
}