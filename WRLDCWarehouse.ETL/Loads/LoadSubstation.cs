using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;
using System;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadSubstation
    {
        public async Task<Substation> LoadSingleAsync(WRLDCWarehouseDbContext _context, SubstationForeign ssForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            Substation existingSS = await _context.Substations.SingleOrDefaultAsync(ss => ss.WebUatId == ssForeign.WebUatId);

            // check if we should not modify existing entities
            if (opt == EntityWriteOption.DontReplace && existingSS != null)
            {
                return existingSS;
            }

            // find the MajorSubstation of the substation via the MajorSubstation WebUatId
            int majorSSebUatId = ssForeign.MajorSubstationWebUatId;
            MajorSubstation majorSS = await _context.MajorSubstations.SingleOrDefaultAsync(mss => mss.WebUatId == majorSSebUatId);
            // if major Substation doesnot exist, skip the import. Ideally, there should not be such case
            if (majorSS == null)
            {
                return null;
            }

            // find the Voltage of the substation via the Voltage WebUatId
            int voltWebUatId = (int)ssForeign.VoltLevelWebUatId;
            VoltLevel voltLevel = await _context.VoltLevels.SingleOrDefaultAsync(vl => vl.WebUatId == voltWebUatId);
            // if voltage level doesnot exist, skip the import. Ideally, there should not be such case
            if (voltLevel == null)
            {
                return null;
            }

            // find the State of the substation via the State WebUatId
            int stateWebUatId = -1;
            if (int.TryParse(ssForeign.StateWebUatId, out int j))
            { stateWebUatId = j; }
            else
            {
                Console.WriteLine($"Could not parse state WebUatId {ssForeign.StateWebUatId}");
            }
            State state = await _context.States.SingleOrDefaultAsync(s => s.WebUatId == stateWebUatId);
            // if state doesnot exist, skip the import. Ideally, there should not be such case
            if (state == null)
            {
                return null;
            }

            // if entity is not present, then insert
            if (existingSS == null)
            {
                Substation newSS = new Substation();
                newSS.Name = ssForeign.Name;
                newSS.VoltLevelId = voltLevel.VoltLevelId;
                newSS.MajorSubstationId = majorSS.MajorSubstationId;
                newSS.StateId = state.StateId;
                newSS.Classification = ssForeign.Classification;
                newSS.BusbarScheme = ssForeign.BusbarScheme;
                newSS.CodDate = ssForeign.CodDate;
                newSS.CommDate = ssForeign.CommDate;
                newSS.DecommDate = ssForeign.DecommDate;
                newSS.WebUatId = ssForeign.WebUatId;

                _context.Substations.Add(newSS);
                await _context.SaveChangesAsync();
                return newSS;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingSS != null)
            {
                _context.Substations.Remove(existingSS);

                Substation newSS = new Substation();
                newSS.Name = ssForeign.Name;
                newSS.VoltLevelId = voltLevel.VoltLevelId;
                newSS.MajorSubstationId = majorSS.MajorSubstationId;
                newSS.StateId = state.StateId;
                newSS.Classification = ssForeign.Classification;
                newSS.BusbarScheme = ssForeign.BusbarScheme;
                newSS.CodDate = ssForeign.CodDate;
                newSS.CommDate = ssForeign.CommDate;
                newSS.DecommDate = ssForeign.DecommDate;
                newSS.WebUatId = ssForeign.WebUatId;

                _context.Substations.Add(newSS);
                await _context.SaveChangesAsync();
                return newSS;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingSS != null)
            {
                existingSS.Name = ssForeign.Name;
                existingSS.VoltLevelId = voltLevel.VoltLevelId;
                existingSS.MajorSubstationId = majorSS.MajorSubstationId;
                existingSS.StateId = state.StateId;
                existingSS.Classification = ssForeign.Classification;
                existingSS.BusbarScheme = ssForeign.BusbarScheme;
                existingSS.CodDate = ssForeign.CodDate;
                existingSS.CommDate = ssForeign.CommDate;
                existingSS.DecommDate = ssForeign.DecommDate;
                await _context.SaveChangesAsync();
                return existingSS;
            }
            return null;
        }
    }
}
