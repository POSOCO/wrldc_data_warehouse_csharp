using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadLineReactor
    {
        public async Task<LineReactor> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, LineReactorForeign lrForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            LineReactor existingLr = await _context.LineReactors.SingleOrDefaultAsync(lr => lr.WebUatId == lrForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingLr != null)
            {
                return existingLr;
            }

            // find the Substation of the LineReactor via the Substation WebUatId
            int ssWebUatId = lrForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == ssWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {ssWebUatId} while inserting LineReactor with webUatId {lrForeign.WebUatId} and name {lrForeign.Name}");
                return null;
            }

            // find the AcTransLineCkt of the LineReactor via the AcTransLineCkt WebUatId
            int lineCktWebUatId = lrForeign.AcTransLineCktWebUatId;
            AcTransLineCkt acCkt = await _context.AcTransLineCkts.SingleOrDefaultAsync(ckt => ckt.WebUatId == lineCktWebUatId);
            // if AcTransLineCkt doesnot exist, skip the import. Ideally, there should not be such case
            if (acCkt == null)
            {
                _log.LogCritical($"Unable to find AcTransLineCkt with webUatId {lineCktWebUatId} while inserting LineReactor with webUatId {lrForeign.WebUatId} and name {lrForeign.Name}");
                return null;
            }

            // find the State of the LineReactor via the State WebUatId
            int stateWebUatId = lrForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                _log.LogCritical($"Unable to find State with webUatId {stateWebUatId} while inserting LineReactor with webUatId {lrForeign.WebUatId} and name {lrForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingLr != null)
            {
                _context.LineReactors.Remove(existingLr);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingLr == null || (opt == EntityWriteOption.Replace && existingLr != null))
            {
                LineReactor newLr = new LineReactor();
                newLr.Name = lrForeign.Name;
                newLr.MvarCapacity = lrForeign.MvarCapacity;
                newLr.CommDate = lrForeign.CommDate;
                newLr.CodDate = lrForeign.CodDate;
                newLr.DecommDate = lrForeign.DecommDate;
                newLr.AcTransLineCktId = acCkt.AcTransLineCktId;
                newLr.SubstationId = substation.SubstationId;
                newLr.StateId = substation.StateId;
                newLr.IsConvertible = lrForeign.IsConvertible == 0 ? false : true;
                newLr.IsSwitchable = lrForeign.IsSwitchable == 0 ? false : true;
                newLr.WebUatId = lrForeign.WebUatId;
                _context.LineReactors.Add(newLr);
                await _context.SaveChangesAsync();
                return newLr;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingLr != null)
            {
                existingLr.Name = lrForeign.Name;
                existingLr.MvarCapacity = lrForeign.MvarCapacity;
                existingLr.CommDate = lrForeign.CommDate;
                existingLr.CodDate = lrForeign.CodDate;
                existingLr.DecommDate = lrForeign.DecommDate;
                existingLr.AcTransLineCktId = acCkt.AcTransLineCktId;
                existingLr.SubstationId = substation.SubstationId;
                existingLr.StateId = substation.StateId;
                existingLr.IsConvertible = lrForeign.IsConvertible == 0 ? false : true;
                existingLr.IsSwitchable = lrForeign.IsSwitchable == 0 ? false : true;
                await _context.SaveChangesAsync();
                return existingLr;
            }
            return null;
        }
    }
}