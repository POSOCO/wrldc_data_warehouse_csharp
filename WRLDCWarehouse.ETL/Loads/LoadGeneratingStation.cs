using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadGeneratingStation
    {
        public async Task<GeneratingStation> LoadSingleAsync(WRLDCWarehouseDbContext _context, GeneratingStationForeign genStationForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            GeneratingStation existingGenStation = await _context.GeneratingStations.SingleOrDefaultAsync(ss => ss.WebUatId == genStationForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingGenStation != null)
            {
                return existingGenStation;
            }

            // find the GeneratorClassification of the substation via the GenClassification WebUatId
            int genClassificationWebUatId = genStationForeign.GenClassificationWebUatId;
            GeneratorClassification genClassification = await _context.GeneratorClassifications.SingleOrDefaultAsync(gc => gc.WebUatId == genClassificationWebUatId);
            // if GeneratorClassification doesnot exist, skip the import. Ideally, there should not be such case
            if (genClassification == null)
            {
                return null;
            }

            // find the Generation Type of the substation via the Voltage WebUatId
            int genTypeWebUatId = genStationForeign.GenerationTypeWebUatId;
            GenerationType genType = await _context.GenerationTypes.SingleOrDefaultAsync(gt => gt.WebUatId == genTypeWebUatId);
            // if GenerationType doesnot exist, skip the import. Ideally, there should not be such case
            if (genType == null)
            {
                return null;
            }

            // find the State of the substation via the State WebUatId
            int stateWebUatId = genStationForeign.StateWebUatId;
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                return null;
            }

            // find the fuel of the substation via the State WebUatId
            int fuelWebUatId = genStationForeign.FuelWebUatId;
            Fuel fuel = await _context.Fuels.SingleOrDefaultAsync(f => f.WebUatId == fuelWebUatId);
            // if fuel doesnot exist, skip the import. Ideally, there should not be such case
            if (fuel == null)
            {
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingGenStation != null)
            {
                _context.GeneratingStations.Remove(existingGenStation);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingGenStation == null || (opt == EntityWriteOption.Replace && existingGenStation != null))
            {
                GeneratingStation genStation = new GeneratingStation();
                genStation.Name = genStationForeign.Name;
                genStation.GenerationTypeId = genType.GenerationTypeId;
                genStation.GeneratorClassificationId = genClassification.GeneratorClassificationId;
                genStation.StateId = state.StateId;
                genStation.FuelId = fuel.FuelId;
                genStation.WebUatId = genStationForeign.WebUatId;

                _context.GeneratingStations.Add(genStation);
                await _context.SaveChangesAsync();
                return genStation;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingGenStation != null)
            {
                existingGenStation.Name = genStationForeign.Name;
                existingGenStation.GenerationTypeId = genType.GenerationTypeId;
                existingGenStation.GeneratorClassificationId = genClassification.GeneratorClassificationId;
                existingGenStation.StateId = state.StateId;
                existingGenStation.FuelId = fuel.FuelId;
                await _context.SaveChangesAsync();
                return existingGenStation;
            }
            return null;
        }
    }
}
