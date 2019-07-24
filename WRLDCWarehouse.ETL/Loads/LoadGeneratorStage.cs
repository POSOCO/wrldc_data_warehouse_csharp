using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadGeneratorStage
    {
        public async Task<GeneratorStage> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, GeneratorStageForeign genStageForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            GeneratorStage existingGenStage = await _context.GeneratorStages.SingleOrDefaultAsync(r => r.WebUatId == genStageForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingGenStage != null)
            {
                return existingGenStage;
            }

            // find the GeneratingStation of the state via the region WebUatId
            int generatingStationWebUatId = genStageForeign.GeneratingStationWebUatId;
            GeneratingStation genStation = await _context.GeneratingStations.SingleOrDefaultAsync(r => r.WebUatId == generatingStationWebUatId);

            // if genStation doesnot exist, skip the import. Ideally, there should not be such case
            if (genStation == null)
            {
                _log.LogCritical($"Could not find GeneratingStation with WebUatId {generatingStationWebUatId} in warehouse while creating GeneratorStage with WebUat Id {genStageForeign.WebUatId} and name {genStageForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingGenStage != null)
            {
                _context.GeneratorStages.Remove(existingGenStage);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingGenStage == null || (opt == EntityWriteOption.Replace && existingGenStage != null))
            {
                GeneratorStage newGenStage = new GeneratorStage();
                newGenStage.Name = genStageForeign.Name;
                newGenStage.GeneratingStationId = genStation.GeneratingStationId;
                newGenStage.WebUatId = genStageForeign.WebUatId;

                _context.GeneratorStages.Add(newGenStage);
                await _context.SaveChangesAsync();
                return newGenStage;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingGenStage != null)
            {
                existingGenStage.Name = genStageForeign.Name;
                existingGenStage.GeneratingStationId = genStation.GeneratingStationId;
                await _context.SaveChangesAsync();
                return existingGenStage;
            }
            return null;
        }
    }
}
