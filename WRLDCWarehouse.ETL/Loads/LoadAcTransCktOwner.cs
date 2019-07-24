using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadAcTransCktOwner
    {
        public async Task<AcTransLineCktOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, AcTransmissionLineCircuitOwnerForeign acCktOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            AcTransLineCktOwner existingAcCktOwner = await _context.AcTransLineCktOwners.SingleOrDefaultAsync(aco => aco.WebUatId == acCktOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingAcCktOwner != null)
            {
                return existingAcCktOwner;
            }

            // find the AcTranLineCkt via the AcTranLineCkt WebUatId
            int acCktWebUatId = acCktOwnerForeign.AcTranLineCktWebUatId;
            AcTransLineCkt acTransCkt = await _context.AcTransLineCkts.SingleOrDefaultAsync(ss => ss.WebUatId == acCktWebUatId);
            // if AcTransLineCkt doesnot exist, skip the import. Ideally, there should not be such case
            if (acTransCkt == null)
            {
                _log.LogCritical($"Could not find AcTransLineCkt with WebUatId {acCktWebUatId} in warehouse while creating AcTransLineCktOwner with WebUat Id {acCktOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the AcTransLineCkt via the Owner WebUatId
            int ownerWebUatId = acCktOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Could not find Owner with WebUatId {ownerWebUatId} in warehouse while creating AcTransLineCktOwner with WebUat Id {acCktOwnerForeign.WebUatId}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingAcCktOwner != null)
            {
                _context.AcTransLineCktOwners.Remove(existingAcCktOwner);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingAcCktOwner == null || (opt == EntityWriteOption.Replace && existingAcCktOwner != null))
            {
                AcTransLineCktOwner acCktOwner = new AcTransLineCktOwner();
                acCktOwner.OwnerId = owner.OwnerId;
                acCktOwner.AcTransLineCktId = acTransCkt.AcTransLineCktId;
                acCktOwner.WebUatId = acCktOwnerForeign.WebUatId;

                _context.AcTransLineCktOwners.Add(acCktOwner);
                await _context.SaveChangesAsync();
                return acCktOwner;
            }

            
            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingAcCktOwner != null)
            {
                existingAcCktOwner.OwnerId = owner.OwnerId;
                existingAcCktOwner.AcTransLineCktId = acTransCkt.AcTransLineCktId;
                await _context.SaveChangesAsync();
                return existingAcCktOwner;
            }
            return null;
        }
    }
}
