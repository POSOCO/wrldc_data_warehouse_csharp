using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignFuels
    {
        public async Task ImportForeignFuels(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            FuelExtract fuelExtract = new FuelExtract();
            List<Fuel> fuels = fuelExtract.ExtractFuels(oracleConnStr);

            LoadFuel loadFuel = new LoadFuel();
            foreach (Fuel fuel in fuels)
            {
                Fuel insertedFuel = await loadFuel.LoadSingleAsync(_context, _log, fuel, opt);
            }
        }
    }
}
