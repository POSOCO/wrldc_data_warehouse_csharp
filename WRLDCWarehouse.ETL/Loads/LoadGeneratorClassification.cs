using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadGeneratorClassification
    {
        public async Task<GeneratorClassification> LoadSingleAsync(WRLDCWarehouseDbContext _context, GeneratorClassification genClassification, EntityWriteOption opt)
        {
            // check if entity already exists
            GeneratorClassification existingGenClassification = await _context.GeneratorClassifications.SingleOrDefaultAsync(gc => gc.WebUatId == genClassification.WebUatId);

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingGenClassification != null)
            {
                _context.GeneratorClassifications.Remove(existingGenClassification);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingGenClassification == null || (opt == EntityWriteOption.Replace && existingGenClassification != null))
            {
                _context.GeneratorClassifications.Add(genClassification);
                await _context.SaveChangesAsync();
                return genClassification;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingGenClassification != null)
            {
                return existingGenClassification;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingGenClassification != null)
            {
                existingGenClassification.Name = genClassification.Name;
                await _context.SaveChangesAsync();
                return existingGenClassification;
            }
            return null;
        }
    }
}
