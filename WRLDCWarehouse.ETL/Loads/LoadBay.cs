using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadBay
    {
        public async Task<Bay> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, BayForeign bayForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            Bay existingBay = await _context.Bays.SingleOrDefaultAsync(b => b.WebUatId == bayForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingBay != null)
            {
                return existingBay;
            }

            // find the BayType via the BayTypeWebUatId
            int bayTypeWebUatId = bayForeign.BayTypeWebUatId;
            BayType bayType = await _context.BayTypes.SingleOrDefaultAsync(ct => ct.WebUatId == bayTypeWebUatId);
            // if BayType doesnot exist, skip the import. Ideally, there should not be such case
            if (bayType == null)
            {
                _log.LogCritical($"Unable to find BayType with webUatId {bayTypeWebUatId} while inserting Bay with webUatId {bayForeign.WebUatId} and name {bayForeign.Name}");
                return null;
            }

            // find the VoltLevel via the VoltLevelWebUatId
            int voltWebUatId = bayForeign.VoltageWebUatId;
            VoltLevel voltLevel = await _context.VoltLevels.SingleOrDefaultAsync(v => v.WebUatId == voltWebUatId);
            // if VoltLevel doesnot exist, skip the import. Ideally, there should not be such case
            if (voltLevel == null)
            {
                _log.LogCritical($"Unable to find voltLevel with webUatId {voltWebUatId} while inserting Bay with webUatId {bayForeign.WebUatId} and name {bayForeign.Name}");
                return null;
            }

            // find the Substation via the SubstationWebUatId
            int substationWebUatId = bayForeign.SubstationWebUatId;
            Substation substation = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == substationWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (substation == null)
            {
                _log.LogCritical($"Unable to find Substation with webUatId {substationWebUatId} while inserting Bay with webUatId {bayForeign.WebUatId} and name {bayForeign.Name}");
                return null;
            }

            async Task<int> getElementId(int entWebUatId, string entType)
            {
                // find the sourceEntityId of the Bay
                int entityId = -1;
                // entityType can be AC LINE CIRCUIT,TCSC,BUS REACTOR,TRANSFORMER,Filter Bank,BUS,FSC,LINE REACTOR
                string entityType = bayForeign.SourceEntityType;
                int elementWebUatId = bayForeign.SourceEntityWebUatId;
                if (entityType == "AC LINE CIRCUIT")
                {
                    // attach element is AcTransLineCkt
                    entityId = (await _context.AcTransLineCkts.SingleOrDefaultAsync(ckt => ckt.WebUatId == elementWebUatId)).AcTransLineCktId;
                }
                else if (entityType == "TCSC")
                {
                    // attach element is Compensator
                    entityId = (await _context.Compensators.SingleOrDefaultAsync(c => c.WebUatId == elementWebUatId)).CompensatorId;
                }
                else if (entityType == "BUS REACTOR")
                {
                    // attach element is BUS REACTOR
                    entityId = (await _context.BusReactors.SingleOrDefaultAsync(br => br.WebUatId == elementWebUatId)).BusReactorId;
                }
                else if (entityType == "TRANSFORMER")
                {
                    // attach element is TRANSFORMER
                    entityId = (await _context.Transformers.SingleOrDefaultAsync(br => br.WebUatId == elementWebUatId)).TransformerId;
                }
                else if (entityType == "Filter Bank")
                {
                    // attach element is Filter Bank
                    entityId = (await _context.FilterBanks.SingleOrDefaultAsync(br => br.WebUatId == elementWebUatId)).FilterBankId;
                }
                else if (entityType == "BUS")
                {
                    // attach element is BUS
                    entityId = (await _context.Buses.SingleOrDefaultAsync(br => br.WebUatId == elementWebUatId)).BusId;
                }
                else if (entityType == "FSC")
                {
                    // attach element is FSC
                    entityId = (await _context.Fscs.SingleOrDefaultAsync(br => br.WebUatId == elementWebUatId)).FscId;
                }
                else if (entityType == "LINE REACTOR")
                {
                    // attach element is LINE REACTOR
                    entityId = (await _context.LineReactors.SingleOrDefaultAsync(br => br.WebUatId == elementWebUatId)).LineReactorId;
                }
                return entityId;
            }

            // find the sourceEntityId of the Bay
            // sourceEntityType can be AC LINE CIRCUIT,TCSC,BUS REACTOR,TRANSFORMER,Filter Bank,BUS,FSC,LINE REACTOR
            string sourceEntityType = bayForeign.SourceEntityType;
            int sourceElementWebUatId = bayForeign.SourceEntityWebUatId;
            int sourceEntityId = await getElementId(sourceElementWebUatId, sourceEntityType);
            if (sourceEntityId == -1)
            {
                // encountered an unknown source element type, ideally this should not happen
                _log.LogCritical($"Encountered an unknown source element type {sourceEntityType} while inserting Bay with webUatId {bayForeign.WebUatId} and name {bayForeign.Name}");
                return null;
            }

            // find the destEntityId of the Bay
            // sourceEntityType can be AC LINE CIRCUIT,TCSC,BUS REACTOR,TRANSFORMER,Filter Bank,BUS,FSC,LINE REACTOR
            string destEntityType = bayForeign.DestEntityType;
            int destEntityId = -1;
            if (destEntityType != null)
            {
                int destElementWebUatId = bayForeign.DestEntityWebUatId;
                destEntityId = await getElementId(destElementWebUatId, destEntityType);
                if (destEntityId == -1)
                {
                    // encountered an unknown destination element type, ideally this should not happen
                    _log.LogCritical($"Encountered an unknown destination element type {destEntityType} while inserting Bay with webUatId {bayForeign.WebUatId} and name {bayForeign.Name}");
                    return null;
                }
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingBay != null)
            {
                _context.Bays.Remove(existingBay);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingBay == null || (opt == EntityWriteOption.Replace && existingBay != null))
            {
                Bay newBay = new Bay();
                newBay.Name = bayForeign.Name;
                newBay.BayNumber = bayForeign.BayNumber;
                newBay.SourceEntityId = sourceEntityId;
                newBay.SourceEntityType = sourceEntityType;
                newBay.SourceEntityName = bayForeign.SourceEntityName;
                if (destEntityType != null)
                {
                    newBay.DestEntityId = destEntityId;
                    newBay.DestEntityType = destEntityType;
                    newBay.DestEntityName = bayForeign.DestEntityName;
                }
                newBay.BayTypeId = bayType.BayTypeId;
                newBay.VoltLevelId = voltLevel.VoltLevelId;
                newBay.SubstationId = substation.SubstationId;
                newBay.WebUatId = bayForeign.WebUatId;
                _context.Bays.Add(newBay);
                await _context.SaveChangesAsync();
                return newBay;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingBay != null)
            {
                existingBay.Name = bayForeign.Name;
                existingBay.BayNumber = bayForeign.BayNumber.ToString();
                existingBay.SourceEntityId = sourceEntityId;
                existingBay.SourceEntityType = sourceEntityType;
                existingBay.SourceEntityName = bayForeign.SourceEntityName;
                if (destEntityType != null)
                {
                    existingBay.DestEntityId = destEntityId;
                    existingBay.DestEntityType = destEntityType;
                    existingBay.DestEntityName = bayForeign.DestEntityName;
                }
                existingBay.BayTypeId = bayType.BayTypeId;
                existingBay.VoltLevelId = voltLevel.VoltLevelId;
                existingBay.SubstationId = substation.SubstationId;
                await _context.SaveChangesAsync();
                return existingBay;
            }
            return null;
        }
    }
}