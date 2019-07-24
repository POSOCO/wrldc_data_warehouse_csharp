using System.Collections.Generic;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignAcTransLineCkts
    {
        public async Task ImportForeignAcTransLineCkts(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            AcTransLineCircuitExtract acTransLineCktExtract = new AcTransLineCircuitExtract();
            List<AcTransmissionLineCircuitForeign> acTransLineCktsForeign = acTransLineCktExtract.ExtractAcTransLineCktForeign(oracleConnStr);

            LoadAcTransmissionLineCkt loadAcTransLineCkt = new LoadAcTransmissionLineCkt();
            foreach (AcTransmissionLineCircuitForeign acTransLineCktForeign in acTransLineCktsForeign)
            {
                AcTransLineCkt insertedAcTransLineCkt = await loadAcTransLineCkt.LoadSingleAsync(_context, _log, acTransLineCktForeign, opt);
            }
        }

        public async Task ImportForeignAcTransLineCktCondTypes(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            AcTransLineCircuitCondTypeExtract acTransLineCktCondExtract = new AcTransLineCircuitCondTypeExtract();
            List<AcTransLineCktCondTypeForeign> acTransLineCktCondTypesForeign = acTransLineCktCondExtract.ExtractAcTransLineCktCondTypeForeign(oracleConnStr);

            LoadAcTransLineCktCondType loadAcTransLineCktCond = new LoadAcTransLineCktCondType();
            foreach (AcTransLineCktCondTypeForeign acTransLineCktCondTypeForeign in acTransLineCktCondTypesForeign)
            {
                AcTransLineCkt insertedAcTransLineCkt = await loadAcTransLineCktCond.LoadSingleAsync(_context, _log, acTransLineCktCondTypeForeign, opt);
            }
        }
    }
}
