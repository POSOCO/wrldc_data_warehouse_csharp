using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadHvdcLineCkt
    {
        public async Task<HvdcLineCkt> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, HvdcLineCktForeign hvdcLineCktForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            HvdcLineCkt existingHvdcLineCkt = await _context.HvdcLineCkts.SingleOrDefaultAsync(ckt => ckt.WebUatId == hvdcLineCktForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingHvdcLineCkt != null)
            {
                return existingHvdcLineCkt;
            }

            // find the HvdcLine of the HvdcLineCkt via the State hvdcLineWebUatId
            int hvdcLineWebUatId = hvdcLineCktForeign.HvdcLineWebUatId;
            HvdcLine hvdcLine = await _context.HvdcLines.SingleOrDefaultAsync(hl => hl.WebUatId == hvdcLineWebUatId);
            // if HvdcLine doesnot exist, skip the import. Ideally, there should not be such case
            if (hvdcLine == null)
            {
                _log.LogCritical($"Unable to find HvdcLine with webUatId {hvdcLineWebUatId} while inserting HvdcLineCkt with webUatId {hvdcLineCktForeign.WebUatId} and name {hvdcLineCktForeign.Name}");
                return null;
            }

            // find the FromBus of the HvdcLineCkt via the FromBusWebUatId
            int fromBusWebUatId = hvdcLineCktForeign.FromBusWebUatId;
            Bus fromBus = await _context.Buses.SingleOrDefaultAsync(b => b.WebUatId == fromBusWebUatId);
            // if FromBus doesnot exist, skip the import. Ideally, there should not be such case
            if (fromBus == null)
            {
                _log.LogCritical($"Unable to find FromBus with webUatId {fromBusWebUatId} while inserting HvdcLineCkt with webUatId {hvdcLineCktForeign.WebUatId} and name {hvdcLineCktForeign.Name}");
                return null;
            }

            // find the TomBus of the HvdcLineCkt via the ToBusWebUatId
            int toBusWebUatId = hvdcLineCktForeign.ToBusWebUatId;
            Bus toBus = await _context.Buses.SingleOrDefaultAsync(b => b.WebUatId == toBusWebUatId);
            // if ToBus doesnot exist, skip the import. Ideally, there should not be such case
            if (toBus == null)
            {
                _log.LogCritical($"Unable to find ToBus with webUatId {toBusWebUatId} while inserting HvdcLineCkt with webUatId {hvdcLineCktForeign.WebUatId} and name {hvdcLineCktForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingHvdcLineCkt != null)
            {
                _context.HvdcLineCkts.Remove(existingHvdcLineCkt);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingHvdcLineCkt == null || (opt == EntityWriteOption.Replace && existingHvdcLineCkt != null))
            {
                HvdcLineCkt newHvdcLineCkt = new HvdcLineCkt();
                newHvdcLineCkt.Name = hvdcLineCktForeign.Name;
                newHvdcLineCkt.CktNumber = hvdcLineCktForeign.CktNumber.ToString();
                newHvdcLineCkt.HvdcLineId = hvdcLine.HvdcLineId;
                newHvdcLineCkt.FromBusId = fromBus.BusId;
                newHvdcLineCkt.ToBusId = toBus.BusId;
                newHvdcLineCkt.NumConductorsPerCkt = hvdcLineCktForeign.NumConductorsPerCkt;
                newHvdcLineCkt.Length = hvdcLineCktForeign.Length;
                newHvdcLineCkt.ThermalLimitMVA = hvdcLineCktForeign.ThermalLimitMVA;
                newHvdcLineCkt.FtcDate = hvdcLineCktForeign.FtcDate;
                newHvdcLineCkt.TrialOperationDate = hvdcLineCktForeign.TrialOperationDate;
                newHvdcLineCkt.CommDate = hvdcLineCktForeign.CommDate;
                newHvdcLineCkt.CodDate = hvdcLineCktForeign.CodDate;
                newHvdcLineCkt.DeCommDate = hvdcLineCktForeign.DeCommDate;

                newHvdcLineCkt.WebUatId = hvdcLineCktForeign.WebUatId;
                _context.HvdcLineCkts.Add(newHvdcLineCkt);
                await _context.SaveChangesAsync();
                return newHvdcLineCkt;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingHvdcLineCkt != null)
            {
                existingHvdcLineCkt.Name = hvdcLineCktForeign.Name;
                existingHvdcLineCkt.CktNumber = hvdcLineCktForeign.CktNumber.ToString();
                existingHvdcLineCkt.HvdcLineId = hvdcLine.HvdcLineId;
                existingHvdcLineCkt.FromBusId = fromBus.BusId;
                existingHvdcLineCkt.ToBusId = toBus.BusId;
                existingHvdcLineCkt.NumConductorsPerCkt = hvdcLineCktForeign.NumConductorsPerCkt;
                existingHvdcLineCkt.Length = hvdcLineCktForeign.Length;
                existingHvdcLineCkt.ThermalLimitMVA = hvdcLineCktForeign.ThermalLimitMVA;
                existingHvdcLineCkt.FtcDate = hvdcLineCktForeign.FtcDate;
                existingHvdcLineCkt.TrialOperationDate = hvdcLineCktForeign.TrialOperationDate;
                existingHvdcLineCkt.CommDate = hvdcLineCktForeign.CommDate;
                existingHvdcLineCkt.CodDate = hvdcLineCktForeign.CodDate;
                existingHvdcLineCkt.DeCommDate = hvdcLineCktForeign.DeCommDate; await _context.SaveChangesAsync();
                return existingHvdcLineCkt;
            }
            return null;
        }
    }
}