using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadTransformerType
    {
        public async Task<TransformerType> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, TransformerType transType, EntityWriteOption opt)
        {
            // check if entity already exists
            TransformerType existingTrType = await _context.TransformerTypes.SingleOrDefaultAsync(trT => trT.WebUatId == transType.WebUatId);

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingTrType != null)
            {
                _context.TransformerTypes.Remove(existingTrType);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingTrType == null || (opt == EntityWriteOption.Replace && existingTrType != null))
            {
                _context.TransformerTypes.Add(transType);
                await _context.SaveChangesAsync();
                return transType;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingTrType != null)
            {
                return existingTrType;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingTrType != null)
            {
                existingTrType.Name = transType.Name;
                await _context.SaveChangesAsync();
                return existingTrType;
            }
            return null;
        }
    }
}
