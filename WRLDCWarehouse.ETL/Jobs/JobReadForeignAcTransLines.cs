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
    public class JobReadForeignAcTransLines
    {
        public async Task ImportForeignAcTransLines(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            AcTransLineExtract acTransLineExtract = new AcTransLineExtract();
            List<AcTransmissionLineForeign> acTransLinesForeign = acTransLineExtract.ExtractAcTransLineForeign(oracleConnStr);

            LoadAcTransmissionLine loadAcTransLine = new LoadAcTransmissionLine();
            foreach (AcTransmissionLineForeign acTransLineForeign in acTransLinesForeign)
            {
                AcTransmissionLine insertedAcTransLine = await loadAcTransLine.LoadSingleAsync(_context, acTransLineForeign, opt);
            }
        }
    }
}
