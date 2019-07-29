using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadTransformer
    {
        public async Task<Transformer> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, TransformerForeign trForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            Transformer existingTr = await _context.Transformers.SingleOrDefaultAsync(tr => tr.WebUatId == trForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingTr != null)
            {
                return existingTr;
            }

            // check if substation type is valid
            string ssTypeSubstation = "SubStation";
            string ssTypeGenStation = "Generating Station";
            if (!(trForeign.StationType == ssTypeSubstation || trForeign.StationType == ssTypeGenStation))
            {
                _log.LogCritical($"substation type is not {ssTypeSubstation} or {ssTypeGenStation} while inserting Transformer with webUatId {trForeign.WebUatId} and name {trForeign.Name}");
                return null;
            }

            MajorSubstation hvSubstation = null;
            int hvSubstationWebUatId = -1;
            if (trForeign.StationType == ssTypeSubstation)
            {
                // The transformer is in Substation
                hvSubstationWebUatId = trForeign.HVStationWebUatId;
                hvSubstation = await _context.MajorSubstations.SingleOrDefaultAsync(hvss => hvss.WebUatId == hvSubstationWebUatId);
                if (hvSubstation == null)
                {
                    _log.LogCritical($"Unable to find MajorSubstation with webUatId {hvSubstationWebUatId} while inserting Transformer with webUatId {trForeign.WebUatId} and name {trForeign.Name}");
                    return null;
                }
            }

            GeneratingStation hvGenStation = null;
            int hvGenstationWebUatId = -1;
            if (trForeign.StationType == ssTypeGenStation)
            {
                // The transformer is in GeneratingStation
                hvGenstationWebUatId = trForeign.HVStationWebUatId;
                hvGenStation = await _context.GeneratingStations.SingleOrDefaultAsync(hvgt => hvgt.WebUatId == hvGenstationWebUatId);
                if (hvGenStation == null)
                {
                    _log.LogCritical($"Unable to find GeneratingStation with webUatId {hvGenstationWebUatId} while inserting Transformer with webUatId {trForeign.WebUatId} and name {trForeign.Name}");
                    return null;
                }
            }

            // find the HV Voltage of the Transformer via the Voltage WebUatId
            int hvVoltWebUatId = trForeign.HighVoltLevelWebUatId;
            VoltLevel hvVolt = await _context.VoltLevels.SingleOrDefaultAsync(vl => vl.WebUatId == hvVoltWebUatId);
            // if voltage level doesnot exist, skip the import. Ideally, there should not be such case
            if (hvVolt == null)
            {
                _log.LogCritical($"Unable to find HV VoltLevel with webUatId {hvVoltWebUatId} while inserting Transformer with webUatId {trForeign.WebUatId} and name {trForeign.Name}");
                return null;
            }

            // find the LV Voltage of the Transformer via the Voltage WebUatId
            int lvVoltWebUatId = trForeign.LowVoltLevelWebUatId;
            VoltLevel lvVolt = await _context.VoltLevels.SingleOrDefaultAsync(vl => vl.WebUatId == lvVoltWebUatId);
            // if voltage level doesnot exist, skip the import. Ideally, there should not be such case
            if (lvVolt == null)
            {
                _log.LogCritical($"Unable to find LV VoltLevel with webUatId {lvVoltWebUatId} while inserting Transformer with webUatId {trForeign.WebUatId} and name {trForeign.Name}");
                return null;
            }

            // find the State of the substation via the State WebUatId
            int stateWebUatId = trForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                _log.LogCritical($"Unable to find State with webUatId {stateWebUatId} while inserting Transformer with webUatId {trForeign.WebUatId} and name {trForeign.Name}");
                return null;
            }

            // find the TransformerType of the Transformer via the TransformerTypeWebUatId
            int trTypeWebUatId = trForeign.TransTypeWebUatId;
            TransformerType trType = await _context.TransformerTypes.SingleOrDefaultAsync(trt => trt.WebUatId == trTypeWebUatId);
            // if TransformerType doesnot exist, skip the import. Ideally, there should not be such case
            if (trType == null)
            {
                _log.LogCritical($"Unable to find TransformerType with webUatId {trTypeWebUatId} while inserting Transformer with webUatId {trForeign.WebUatId} and name {trForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingTr != null)
            {
                _context.Transformers.Remove(existingTr);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingTr == null || (opt == EntityWriteOption.Replace && existingTr != null))
            {
                Transformer newTr = new Transformer();
                newTr.Name = trForeign.Name;
                newTr.StationType = trForeign.StationType;
                newTr.HighVoltLevelId = hvVolt.VoltLevelId;
                newTr.LowVoltLevelId = lvVolt.VoltLevelId;
                newTr.TransformerNumber = trForeign.TransformerNumber;
                newTr.TransformerTypeId = trType.TransformerTypeId;
                newTr.StateId = state.StateId;
                newTr.MVACapacity = trForeign.MVACapacity;
                newTr.CodDate = trForeign.CodDate;
                newTr.CommDate = trForeign.CommDate;
                newTr.DecommDate = trForeign.DecommDate;
                newTr.WebUatId = trForeign.WebUatId;
                if (trForeign.StationType == ssTypeSubstation)
                {
                    newTr.HvSubstationId = hvSubstation.MajorSubstationId;
                }
                else if (trForeign.StationType == ssTypeGenStation)
                {
                    newTr.HvGeneratingStationId = hvGenStation.GeneratingStationId;
                }

                _context.Transformers.Add(newTr);
                await _context.SaveChangesAsync();
                return newTr;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingTr != null)
            {
                existingTr.Name = trForeign.Name;
                existingTr.StationType = trForeign.StationType;
                existingTr.HighVoltLevelId = hvVolt.VoltLevelId;
                existingTr.LowVoltLevelId = lvVolt.VoltLevelId;
                existingTr.TransformerNumber = trForeign.TransformerNumber;
                existingTr.TransformerTypeId = trType.TransformerTypeId;
                existingTr.StateId = state.StateId;
                existingTr.MVACapacity = trForeign.MVACapacity;
                existingTr.CodDate = trForeign.CodDate;
                existingTr.CommDate = trForeign.CommDate;
                existingTr.DecommDate = trForeign.DecommDate;
                if (trForeign.StationType == ssTypeSubstation)
                {
                    existingTr.HvSubstationId = hvSubstation.MajorSubstationId;
                }
                else if (trForeign.StationType == ssTypeGenStation)
                {
                    existingTr.HvGeneratingStationId = hvGenStation.GeneratingStationId;
                }
                await _context.SaveChangesAsync();
                return existingTr;
            }
            return null;
        }
    }
}