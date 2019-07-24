using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadAcTransmissionLine
    {
        public async Task<AcTransmissionLine> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, AcTransmissionLineForeign acTrForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            AcTransmissionLine existingAcTrLine = await _context.AcTransmissionLines.SingleOrDefaultAsync(acTr => acTr.WebUatId == acTrForeign.WebUatId);

            // check if we should not modify existing entity
            if (opt == EntityWriteOption.DontReplace && existingAcTrLine != null)
            {
                return existingAcTrLine;
            }

            // find the from Substation via FromSubstattion WebUatId
            int fromSSebUatId = acTrForeign.FromSSWebUatId;
            Substation fromSS = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == fromSSebUatId);
            // if FromSubstation doesnot exist, skip the import. Ideally, there should not be such case
            if (fromSS == null)
            {
                _log.LogCritical($"Unable to find FromSubstation with webUatId {fromSSebUatId} while inserting AcTransmissionLine with webUatId {acTrForeign.WebUatId} and name {acTrForeign.Name}");
                return null;
            }

            // find the To Substation via ToSubstattion WebUatId
            int toSSebUatId = acTrForeign.ToSSWebUatId;
            Substation toSS = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == toSSebUatId);
            // if ToSubstation doesnot exist, skip the import. Ideally, there should not be such case
            if (toSS == null)
            {
                _log.LogCritical($"Unable to find ToSubstation with webUatId {toSSebUatId} while inserting AcTransmissionLine with webUatId {acTrForeign.WebUatId} and name {acTrForeign.Name}");
                return null;
            }

            // find the Voltage of the substation via the Voltage WebUatId
            int voltWebUatId = acTrForeign.VoltLevelWebUatId;
            VoltLevel voltLevel = await _context.VoltLevels.SingleOrDefaultAsync(vl => vl.WebUatId == voltWebUatId);
            // if voltage level doesnot exist, skip the import. Ideally, there should not be such case
            if (voltLevel == null)
            {
                _log.LogCritical($"Unable to find VoltLevel with webUatId {voltWebUatId} while inserting AcTransmissionLine with webUatId {acTrForeign.WebUatId} and name {acTrForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingAcTrLine != null)
            {
                _context.AcTransmissionLines.Remove(existingAcTrLine);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingAcTrLine == null || (opt == EntityWriteOption.Replace && existingAcTrLine != null))
            {
                AcTransmissionLine newAcTrLine = new AcTransmissionLine();
                newAcTrLine.Name = acTrForeign.Name;
                newAcTrLine.VoltLevelId = voltLevel.VoltLevelId;
                newAcTrLine.FromSubstationId = fromSS.SubstationId;
                newAcTrLine.ToSubstationId = toSS.SubstationId;
                newAcTrLine.WebUatId = acTrForeign.WebUatId;

                _context.AcTransmissionLines.Add(newAcTrLine);
                await _context.SaveChangesAsync();
                return newAcTrLine;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingAcTrLine != null)
            {
                existingAcTrLine.Name = acTrForeign.Name;
                existingAcTrLine.VoltLevelId = voltLevel.VoltLevelId;
                existingAcTrLine.FromSubstationId = fromSS.SubstationId;
                existingAcTrLine.ToSubstationId = toSS.SubstationId;
                await _context.SaveChangesAsync();
                return existingAcTrLine;
            }
            return null;
        }
    }
}
