using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;
using System;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadCompensator
    {
        public async Task<Compensator> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, CompensatorForeign compensatorForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            Compensator existingCompensator = await _context.Compensators.SingleOrDefaultAsync(lr => lr.WebUatId == compensatorForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingCompensator != null)
            {
                return existingCompensator;
            }

            // find the CompensatorType via the CompensatorTypeWebUatId
            int compTypeWebUatId = compensatorForeign.CompensatorTypeWebUatId;
            CompensatorType compType = await _context.CompensatorTypes.SingleOrDefaultAsync(ct => ct.WebUatId == compTypeWebUatId);
            // if CompensatorType doesnot exist, skip the import. Ideally, there should not be such case
            if (compType == null)
            {
                _log.LogCritical($"Unable to find CompensatorType with webUatId {compTypeWebUatId} while inserting Compensator with webUatId {compensatorForeign.WebUatId} and name {compensatorForeign.Name}");
                return null;
            }

            // find the Substation via the SubstationWebUatId
            int substationWebUatId = compensatorForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == substationWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {substationWebUatId} while inserting Compensator with webUatId {compensatorForeign.WebUatId} and name {compensatorForeign.Name}");
                return null;
            }

            // find the State via the State WebUatId
            int stateWebUatId = compensatorForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                _log.LogCritical($"Unable to find state with webUatId {stateWebUatId} while inserting Compensator with webUatId {compensatorForeign.WebUatId} and name {compensatorForeign.Name}");
                return null;
            }

            // find the attachElement Id of the Compensator
            int attachElementId = -1;
            int attachElementType = compensatorForeign.AttachElementType;
            int attachElementWebUatId = compensatorForeign.AttachElementWebUatId;
            if (attachElementType == 1)
            {
                // attach element is a bus
                attachElementId = (await _context.Buses.SingleOrDefaultAsync(b => b.WebUatId == attachElementWebUatId)).BusId;
            }
            else if (attachElementType == 2)
            {
                // attach element is an AC Transmission line
                attachElementId = (await _context.AcTransLineCkts.SingleOrDefaultAsync(b => b.WebUatId == attachElementWebUatId)).AcTransLineCktId;
            }
            else
            {
                // encountered an unknown attach element type, ideally this should not happen
                _log.LogCritical($"Encountered an unknown attach element type {attachElementType} while inserting Compensator with webUatId {compensatorForeign.WebUatId} and name {compensatorForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingCompensator != null)
            {
                _context.Compensators.Remove(existingCompensator);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingCompensator == null || (opt == EntityWriteOption.Replace && existingCompensator != null))
            {
                Compensator newCompensator = new Compensator();
                newCompensator.Name = compensatorForeign.Name;
                newCompensator.CompensatorTypeId = compType.CompensatorTypeId;
                newCompensator.SubstationId = substation.SubstationId;
                newCompensator.StateId = state.StateId;
                newCompensator.AttachElementType = compensatorForeign.AttachElementType;
                newCompensator.AttachElementId = attachElementId;
                newCompensator.CompensatorNumber = compensatorForeign.CompensatorNumber.ToString();
                newCompensator.PercVariableComp = compensatorForeign.PercVariableComp;
                newCompensator.PercFixedComp = compensatorForeign.PercFixedComp;
                newCompensator.LineReactanceOhms = compensatorForeign.LineReactanceOhms;
                newCompensator.CommDate = compensatorForeign.CommDate;
                newCompensator.CodDate = compensatorForeign.CodDate;
                newCompensator.DeCommDate = compensatorForeign.DeCommDate;
                newCompensator.WebUatId = compensatorForeign.WebUatId;
                _context.Compensators.Add(newCompensator);
                await _context.SaveChangesAsync();
                return newCompensator;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingCompensator != null)
            {
                existingCompensator.Name = compensatorForeign.Name;
                existingCompensator.CompensatorTypeId = compType.CompensatorTypeId;
                existingCompensator.SubstationId = substation.SubstationId;
                existingCompensator.StateId = state.StateId;
                existingCompensator.AttachElementType = compensatorForeign.AttachElementType;
                existingCompensator.AttachElementId = attachElementId;
                existingCompensator.CompensatorNumber = compensatorForeign.CompensatorNumber.ToString();
                existingCompensator.PercVariableComp = compensatorForeign.PercVariableComp;
                existingCompensator.PercFixedComp = compensatorForeign.PercFixedComp;
                existingCompensator.LineReactanceOhms = compensatorForeign.LineReactanceOhms;
                existingCompensator.CommDate = compensatorForeign.CommDate;
                existingCompensator.CodDate = compensatorForeign.CodDate;
                existingCompensator.DeCommDate = compensatorForeign.DeCommDate;
                await _context.SaveChangesAsync();
                return existingCompensator;
            }
            return null;
        }
    }
}