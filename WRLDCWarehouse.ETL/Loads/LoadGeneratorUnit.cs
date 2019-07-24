using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadGeneratorUnit
    {
        public async Task<GeneratorUnit> LoadSingleAsync(WRLDCWarehouseDbContext _context, ILogger _log, GeneratorUnitForeign genUnitForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            GeneratorUnit existingGenUnit = await _context.GeneratorUnits.SingleOrDefaultAsync(r => r.WebUatId == genUnitForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingGenUnit != null)
            {
                return existingGenUnit;
            }

            // find the GeneratingStation of the unit via the GeneratingStationWebUatId
            int generatingStationWebUatId = genUnitForeign.GeneratingStationWebUatId;
            GeneratingStation genStation = await _context.GeneratingStations.SingleOrDefaultAsync(r => r.WebUatId == generatingStationWebUatId);

            // if genStation doesnot exist, skip the import. Ideally, there should not be such case
            if (genStation == null)
            {
                _log.LogCritical($"Could not find GeneratingStation with WebUatId {generatingStationWebUatId} in warehouse while creating GeneratorUnit with WebUat Id {genUnitForeign.WebUatId} and name {genUnitForeign.Name}");
                return null;
            }

            // find the GeneratorStage of the unit via the GeneratingStationWebUatId
            int generatorStageWebUatId = genUnitForeign.GeneratorStageWebUatId;
            GeneratorStage genStage = await _context.GeneratorStages.SingleOrDefaultAsync(r => r.WebUatId == generatorStageWebUatId);

            // if genStage doesnot exist, skip the import. Ideally, there should not be such case
            if (genStage == null)
            {
                _log.LogCritical($"Could not find GeneratorStage with WebUatId {generatorStageWebUatId} in warehouse while creating GeneratorUnit with WebUat Id {genUnitForeign.WebUatId} and name {genUnitForeign.Name}");
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingGenUnit != null)
            {
                _context.GeneratorUnits.Remove(existingGenUnit);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingGenUnit == null || (opt == EntityWriteOption.Replace && existingGenUnit != null))
            {
                GeneratorUnit newGenUnit = new GeneratorUnit();
                newGenUnit.Name = genUnitForeign.Name;
                newGenUnit.GeneratingStationId = genStation.GeneratingStationId;
                newGenUnit.GeneratorStageId = genStage.GeneratorStageId;
                newGenUnit.UnitNumber = genUnitForeign.UnitNumber.ToString();
                newGenUnit.GenVoltageKV = genUnitForeign.GenVoltageKV;
                newGenUnit.GenHighVoltageKV = genUnitForeign.GenHighVoltageKV;
                newGenUnit.MvaCapacity = genUnitForeign.MvaCapacity;
                newGenUnit.InstalledCapacity = genUnitForeign.InstalledCapacity;
                newGenUnit.CodDateTime = genUnitForeign.CodDateTime;
                newGenUnit.CommDateTime = genUnitForeign.CommDateTime;
                newGenUnit.DeCommDateTime = genUnitForeign.DeCommDateTime;
                newGenUnit.WebUatId = genUnitForeign.WebUatId;

                _context.GeneratorUnits.Add(newGenUnit);
                await _context.SaveChangesAsync();
                return newGenUnit;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingGenUnit != null)
            {
                existingGenUnit.Name = genUnitForeign.Name;
                existingGenUnit.GeneratingStationId = genStation.GeneratingStationId;
                existingGenUnit.GeneratorStageId = genStage.GeneratorStageId;
                existingGenUnit.UnitNumber = genUnitForeign.UnitNumber.ToString();
                existingGenUnit.GenVoltageKV = genUnitForeign.GenVoltageKV;
                existingGenUnit.GenHighVoltageKV = genUnitForeign.GenHighVoltageKV;
                existingGenUnit.MvaCapacity = genUnitForeign.MvaCapacity;
                existingGenUnit.InstalledCapacity = genUnitForeign.InstalledCapacity;
                existingGenUnit.CodDateTime = genUnitForeign.CodDateTime;
                existingGenUnit.CommDateTime = genUnitForeign.CommDateTime;
                existingGenUnit.DeCommDateTime = genUnitForeign.DeCommDateTime;
                await _context.SaveChangesAsync();
                return existingGenUnit;
            }
            return null;
        }
    }
}
