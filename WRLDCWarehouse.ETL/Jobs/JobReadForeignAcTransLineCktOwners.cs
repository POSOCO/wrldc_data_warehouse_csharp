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
    public class JobReadForeignAcTransLineCktOwners
    {
        public async Task ImportForeignAcTransLineCktOwners(WRLDCWarehouseDbContext _context, string oracleConnStr, EntityWriteOption opt)
        {
            AcTransLineCktOwnerExtract acCktOwnerExtract = new AcTransLineCktOwnerExtract();
            List<AcTransmissionLineCircuitOwnerForeign> acCktOwnersForeign = acCktOwnerExtract.ExtractAcTransLineCktOwnersForeign(oracleConnStr);

            LoadAcTransCktOwner loadAcCktOwner = new LoadAcTransCktOwner();
            foreach (AcTransmissionLineCircuitOwnerForeign acCktOwnerForeign in acCktOwnersForeign)
            {
                AcTransLineCktOwner insertedAcCktOwner = await loadAcCktOwner.LoadSingleAsync(_context, acCktOwnerForeign, opt);
            }
        }
    }
}
