using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadGenerationTypeFuel
    {
        public async Task<GenerationTypeFuel> LoadSingleAsync(WRLDCWarehouseDbContext _context, GenerationTypeFuelForeign genTypeFuelForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            GenerationTypeFuel existingGenTypeFuel = await _context.GenerationTypeFuels.SingleOrDefaultAsync(gtf => gtf.WebUatId == genTypeFuelForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingGenTypeFuel != null)
            {
                return existingGenTypeFuel;
            }

            // find the GenerationType via the Substation WebUatId
            int genTypeWebUatId = genTypeFuelForeign.GenerationTypeWebUatId;
            GenerationType genType = await _context.GenerationTypes.SingleOrDefaultAsync(gt => gt.WebUatId == genTypeWebUatId);
            // if GenerationType doesnot exist, skip the import. Ideally, there should not be such case
            if (genType == null)
            {
                return null;
            }

            // find the Fuel via the Fuel WebUatId
            int fuelWebUatId = genTypeFuelForeign.FuelWebUatId;
            Fuel fuel = await _context.Fuels.SingleOrDefaultAsync(f => f.WebUatId == fuelWebUatId);
            // if fuel doesnot exist, skip the import. Ideally, there should not be such case
            if (fuel == null)
            {
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingGenTypeFuel != null)
            {
                _context.GenerationTypeFuels.Remove(existingGenTypeFuel);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingGenTypeFuel == null || (opt == EntityWriteOption.Replace && existingGenTypeFuel != null))
            {
                GenerationTypeFuel newGenTypeFuel = new GenerationTypeFuel();
                newGenTypeFuel.FuelId = fuel.FuelId;
                newGenTypeFuel.GenerationTypeId = genType.GenerationTypeId;
                newGenTypeFuel.WebUatId = genTypeFuelForeign.WebUatId;

                _context.GenerationTypeFuels.Add(newGenTypeFuel);
                await _context.SaveChangesAsync();
                return newGenTypeFuel;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingGenTypeFuel != null)
            {
                existingGenTypeFuel.FuelId = fuel.FuelId;
                existingGenTypeFuel.GenerationTypeId = genType.GenerationTypeId;
                await _context.SaveChangesAsync();
                return existingGenTypeFuel;
            }
            return null;
        }
    }
}
