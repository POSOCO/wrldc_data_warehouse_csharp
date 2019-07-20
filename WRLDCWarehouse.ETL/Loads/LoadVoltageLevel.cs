using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadVoltageLevel
    {
        public async Task<VoltLevel> LoadSingleAsync(WRLDCWarehouseDbContext _context, VoltLevel voltLevel, EntityWriteOption opt)
        {
            // check if frequencySummary already exists for the date and delete it
            VoltLevel existingVoltLevel = await _context.VoltLevels.SingleOrDefaultAsync(v => v.WebUatId == voltLevel.WebUatId);

            // if volt level is not present, then insert
            if (existingVoltLevel == null)
            {
                _context.Add(voltLevel);
                await _context.SaveChangesAsync();
                return voltLevel;
            }

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingVoltLevel != null)
            {
                return existingVoltLevel;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingVoltLevel != null)
            {
                _context.VoltLevels.Remove(existingVoltLevel);
                _context.Add(voltLevel);
                await _context.SaveChangesAsync();
                return voltLevel;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingVoltLevel != null)
            {
                existingVoltLevel.Name = voltLevel.Name;
                existingVoltLevel.EntityType = voltLevel.EntityType;
                await _context.SaveChangesAsync();
                return existingVoltLevel;
            }
            return null;
        }
    }
}
