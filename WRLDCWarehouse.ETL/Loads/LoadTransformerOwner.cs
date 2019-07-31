using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadTransformerOwner
    {
        public async Task<TransformerOwner> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, TransformerOwnerForeign trOwnerForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            TransformerOwner existingTrOwner = await _context.TransformerOwners.SingleOrDefaultAsync(trO => trO.WebUatId == trOwnerForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingTrOwner != null)
            {
                return existingTrOwner;
            }

            // find the Transformer via the TransformerWebUatId
            int trWebUatId = trOwnerForeign.TransformerWebUatId;
            Transformer transformer = await _context.Transformers.SingleOrDefaultAsync(tr => tr.WebUatId == trWebUatId);
            // if Transformer doesnot exist, skip the import. Ideally, there should not be such case
            if (transformer == null)
            {
                _log.LogCritical($"Unable to find Transformer with webUatId {trWebUatId} while inserting TransformerOwner with webUatId {trOwnerForeign.WebUatId}");
                return null;
            }

            // find the Owner of the substation via the Owner WebUatId
            int ownerWebUatId = trOwnerForeign.OwnerWebUatId;
            Owner owner = await _context.Owners.SingleOrDefaultAsync(o => o.WebUatId == ownerWebUatId);
            // if owner doesnot exist, skip the import. Ideally, there should not be such case
            if (owner == null)
            {
                _log.LogCritical($"Unable to find Owner with webUatId {ownerWebUatId} while inserting TransformerOwner with webUatId {trOwnerForeign.WebUatId}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingTrOwner != null)
            {
                _context.TransformerOwners.Remove(existingTrOwner);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingTrOwner == null || (opt == EntityWriteOption.Replace && existingTrOwner != null))
            {
                TransformerOwner newTrOwner = new TransformerOwner();
                newTrOwner.OwnerId = owner.OwnerId;
                newTrOwner.TransformerId = transformer.TransformerId;
                newTrOwner.WebUatId = trOwnerForeign.WebUatId;

                _context.TransformerOwners.Add(newTrOwner);
                await _context.SaveChangesAsync();
                return newTrOwner;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingTrOwner != null)
            {
                existingTrOwner.OwnerId = owner.OwnerId;
                existingTrOwner.TransformerId = transformer.TransformerId;
                await _context.SaveChangesAsync();
                return existingTrOwner;
            }
            return null;
        }
    }
}
