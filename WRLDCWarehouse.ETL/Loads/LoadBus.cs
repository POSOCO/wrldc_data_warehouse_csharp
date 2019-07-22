using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadBus
    {
        public async Task<Bus> LoadSingleAsync(WRLDCWarehouseDbContext _context, BusForeign busForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            Bus existingBus = await _context.Buses.SingleOrDefaultAsync(b => b.WebUatId == busForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingBus != null)
            {
                return existingBus;
            }

            // find the substation of the bus via the AssSubstation WebUatId
            int subWebUatId = busForeign.AssSubstationWebUatId;
            Substation busSubstation = await _context.Substations.SingleOrDefaultAsync(s => s.WebUatId == subWebUatId);
            // if Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (busSubstation == null)
            {
                return null;
            }

            // find the substation of the bus via the AssSubstation WebUatId
            int voltLevelWebUatId = busForeign.VoltageWebUatId;
            VoltLevel voltLevel = await _context.VoltLevels.SingleOrDefaultAsync(v => v.WebUatId == voltLevelWebUatId);
            // if voltLevel doesnot exist, skip the import. Ideally, there should not be such case
            if (voltLevel == null)
            {
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingBus != null)
            {
                _context.Buses.Remove(existingBus);
            }

            // if entity is not present, then insert
            if (existingBus == null || (opt == EntityWriteOption.Replace && existingBus != null))
            {
                Bus newBus = new Bus();
                newBus.Name = busForeign.Name;
                newBus.BusNumber = busForeign.BusNumber.ToString();
                newBus.SubstationId = busSubstation.SubstationId;
                newBus.VoltLevelId = voltLevel.VoltLevelId;
                newBus.WebUatId = busForeign.WebUatId;

                _context.Buses.Add(newBus);
                await _context.SaveChangesAsync();
                return newBus;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingBus != null)
            {
                existingBus.Name = busForeign.Name;
                existingBus.BusNumber = busForeign.BusNumber.ToString();
                existingBus.SubstationId = busSubstation.SubstationId;
                existingBus.VoltLevelId = voltLevel.VoltLevelId;
                await _context.SaveChangesAsync();
                return existingBus;
            }
            return null;
        }
    }
}
