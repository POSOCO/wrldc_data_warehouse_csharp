using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadAcTransLineCktCondType
    {
        public async Task<AcTransLineCkt> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, AcTransLineCktCondTypeForeign acLineCondTypeForeign, EntityWriteOption opt)
        {
            // get the conductor type of the entity
            ConductorType condType = await _context.ConductorTypes.SingleOrDefaultAsync(ct => ct.WebUatId == acLineCondTypeForeign.CondTypeWebUatId);
            // if conductor type doesnot exist, skip the import. Ideally, there should not be such case
            if (condType == null)
            {
                _log.LogCritical($"Unable to find ConductorType with webUatId {acLineCondTypeForeign.CondTypeWebUatId} while inserting AcTransLineCktCondType with webUatId {acLineCondTypeForeign.WebUatId}");
                return null;
            }

            // get the ac transmission line ckt of the entity
            AcTransLineCkt existingAcTransLineCkt = await _context.AcTransLineCkts.SingleOrDefaultAsync(acCkt => acCkt.WebUatId == acLineCondTypeForeign.AcTransLineCktWebUatId);
            // if ac transmission line ckt doesnot exist, skip the import. Ideally, there should not be such case
            if (existingAcTransLineCkt == null)
            {
                _log.LogCritical($"Unable to find AcTransLineCkt with webUatId {acLineCondTypeForeign.AcTransLineCktWebUatId} while inserting AcTransLineCktCondType with webUatId {acLineCondTypeForeign.WebUatId}");
                return null;
            }

            // check if we should not modify existing conductor type
            if (existingAcTransLineCkt.ConductorTypeId.HasValue && opt == EntityWriteOption.DontReplace)
            {
                return existingAcTransLineCkt;
            }

            // if conductor type is not present, then insert or replace conductor type
            if (!existingAcTransLineCkt.ConductorTypeId.HasValue || opt == EntityWriteOption.Replace || opt == EntityWriteOption.Modify)
            {
                existingAcTransLineCkt.ConductorTypeId = condType.ConductorTypeId;
                await _context.SaveChangesAsync();
                return existingAcTransLineCkt;
            }

            return null;
        }
    }
}
