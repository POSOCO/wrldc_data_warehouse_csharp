using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using WRLDCWarehouse.Core.ForiegnEntities;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignGeneratingStations
    {
        public async Task ImportForeignGeneratingStations(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            GeneratingStationExtract genStationExtract = new GeneratingStationExtract();
            List<GeneratingStationForeign> genStationsForeign = genStationExtract.ExtractGeneratingStationsForeign(oracleConnStr);

            LoadGeneratingStation loadGenStation = new LoadGeneratingStation();
            foreach (GeneratingStationForeign genStationForeign in genStationsForeign)
            {
                GeneratingStation insertedGenStation = await loadGenStation.LoadSingleAsync(_context, genStationForeign, opt);
            }
        }
    }
}
