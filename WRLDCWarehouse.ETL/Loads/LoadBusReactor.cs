using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadBusReactor
    {
        public async Task<BusReactor> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, BusReactorForeign brForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            BusReactor existingBr = await _context.BusReactors.SingleOrDefaultAsync(br => br.WebUatId == brForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingBr != null)
            {
                return existingBr;
            }

            // find the Substation of the BusReactor via the Substation WebUatId
            int ssWebUatId = brForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == ssWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {ssWebUatId} while inserting BusReactor with webUatId {brForeign.WebUatId} and name {brForeign.Name}");
                return null;
            }

            // find the Bus of the BusReactor via the Bus WebUatId
            int busWebUatId = brForeign.BusWebUatId;
            Bus bus = await _context.Buses.SingleOrDefaultAsync(b => b.WebUatId == busWebUatId);
            // if Bus doesnot exist, skip the import. Ideally, there should not be such case
            if (bus == null)
            {
                _log.LogCritical($"Unable to find Bus with webUatId {busWebUatId} while inserting BusReactor with webUatId {brForeign.WebUatId} and name {brForeign.Name}");
                return null;
            }

            // find the State of the BusReactor via the State WebUatId
            int stateWebUatId = brForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                _log.LogCritical($"Unable to find State with webUatId {stateWebUatId} while inserting Transformer with webUatId {brForeign.WebUatId} and name {brForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingBr != null)
            {
                _context.BusReactors.Remove(existingBr);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingBr == null || (opt == EntityWriteOption.Replace && existingBr != null))
            {
                BusReactor newBr = new BusReactor();
                newBr.Name = brForeign.Name;
                newBr.BusReactorNumber = brForeign.BusReactorNumber;
                newBr.MvarCapacity = brForeign.MvarCapacity;
                newBr.CommDate = brForeign.CommDate;
                newBr.CodDate = brForeign.CodDate;
                newBr.DecommDate = brForeign.DecommDate;
                newBr.BusId = bus.BusId;
                newBr.SubstationId = substation.SubstationId;
                newBr.StateId = substation.StateId;
                newBr.WebUatId = brForeign.WebUatId;
                _context.BusReactors.Add(newBr);
                await _context.SaveChangesAsync();
                return newBr;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingBr != null)
            {
                existingBr.Name = brForeign.Name;
                existingBr.BusReactorNumber = brForeign.BusReactorNumber;
                existingBr.MvarCapacity = brForeign.MvarCapacity;
                existingBr.CommDate = brForeign.CommDate;
                existingBr.CodDate = brForeign.CodDate;
                existingBr.DecommDate = brForeign.DecommDate;
                existingBr.BusId = bus.BusId;
                existingBr.SubstationId = substation.SubstationId;
                existingBr.StateId = substation.StateId;
                await _context.SaveChangesAsync();
                return existingBr;
            }
            return null;
        }
    }
}