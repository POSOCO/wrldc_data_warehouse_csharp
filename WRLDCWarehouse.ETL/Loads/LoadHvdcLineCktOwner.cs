using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadHvdcLineCktOwner
    {
        public async Task<HvdcLineCktOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, HvdcLineCktOwnerForeign cktOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            HvdcLineCktOwner existingCktOwner = await _context.HvdcLineCktOwners.SingleOrDefaultAsync(cO => cO.WebUatId == cktOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingCktOwner != null)
            {
                return existingCktOwner;
            }

            // find the HvdcLineCkt via the HvdcLineCkt WebUatId
            int cktWebUatId = cktOwnerForeign.HvdcLineCktWebUatId;
            HvdcLineCkt ckt = await _context.HvdcLineCkts.SingleOrDefaultAsync(c => c.WebUatId == cktWebUatId);
            // if HvdcLineCkt doesnot exist, skip the import. Ideally, there should not be such case
            if (ckt == null)
            {
                _log.LogCritical($"Unable to find HvdcLineCkt with webUatId {cktWebUatId} while inserting HvdcLineCktOwner with webUatId {cktOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the substation via the Owner WebUatId
            int ownerWebUatId = cktOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting HvdcLineCktOwner with webUatId {cktOwnerForeign.WebUatId}");
                return null;
            }

            try
            {
                // check if we have to replace the entity completely
                if (opt == EntityWriteOption.Replace && existingCktOwner != null)
                {
                    _context.HvdcLineCktOwners.Remove(existingCktOwner);
                }

                // if entity is not present, then insert or check if we have to replace the entity completely
                if (existingCktOwner == null || (opt == EntityWriteOption.Replace && existingCktOwner != null))
                {
                    HvdcLineCktOwner newCktOwner = new HvdcLineCktOwner();
                    newCktOwner.OwnerId = owner.OwnerId;
                    newCktOwner.HvdcLineCktId = ckt.HvdcLineCktId;
                    newCktOwner.WebUatId = cktOwnerForeign.WebUatId;

                    _context.HvdcLineCktOwners.Add(newCktOwner);
                    await _context.SaveChangesAsync();
                    return newCktOwner;
                }

                // check if we have to modify the entity
                if (opt == EntityWriteOption.Modify && existingCktOwner != null)
                {
                    existingCktOwner.OwnerId = owner.OwnerId;
                    existingCktOwner.HvdcLineCktId = ckt.HvdcLineCktId;
                    await _context.SaveChangesAsync();
                    return existingCktOwner;
                }
            }
            catch (DbUpdateException e)
            {
                _log.LogCritical($"Error occured while inserting HvdcLineCktOwner with webUatId {cktOwnerForeign.WebUatId}, owner id {owner.OwnerId} and cktId {ckt.HvdcLineCktId}");
                _log.LogCritical($"EntityWriteOption = {opt.ToString()}");
                _log.LogCritical($"{e.Message}");
                return null;
            }

            return null;
        }
    }
}
