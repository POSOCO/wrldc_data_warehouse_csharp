using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.Core.ForiegnEntities;
using Microsoft.EntityFrameworkCore;
using WRLDCWarehouse.ETL.Enums;

namespace WRLDCWarehouse.ETL.Loads
{
    public class LoadAcTransmissionLineCkt
    {
        public async Task<AcTransLineCkt> LoadSingleAsync(WRLDCWarehouseDbContext _context, AcTransmissionLineCircuitForeign acTrLineCktForeign, EntityWriteOption opt)
        {
            // check if entity already exists
            AcTransLineCkt existingAcTrLineCkt = await _context.AcTransLineCkts.SingleOrDefaultAsync(acCkt => acCkt.WebUatId == acTrLineCktForeign.WebUatId);

            // check if we should not modify existing entity
            if (opt == EntityWriteOption.DontReplace && existingAcTrLineCkt != null)
            {
                return existingAcTrLineCkt;
            }

            // find the from AcTransmissionLine via FromSubstattion WebUatId
            int acTransLineWebUatId = acTrLineCktForeign.AcTransLineWebUatId;
            AcTransmissionLine acTrLine = await _context.AcTransmissionLines.SingleOrDefaultAsync(atl => atl.WebUatId == acTransLineWebUatId);
            // if AcTransmissionLine doesnot exist, skip the import. Ideally, there should not be such case
            if (acTrLine == null)
            {
                return null;
            }

            // check if we have to replace the entity completely
            if (opt == EntityWriteOption.Replace && existingAcTrLineCkt != null)
            {
                _context.AcTransLineCkts.Remove(existingAcTrLineCkt);
            }

            // if entity is not present, then insert or check if we have to replace the entity completely
            if (existingAcTrLineCkt == null || (opt == EntityWriteOption.Replace && existingAcTrLineCkt != null))
            {
                AcTransLineCkt newAcTrLineCkt = new AcTransLineCkt();
                newAcTrLineCkt.Name = acTrLineCktForeign.Name;
                newAcTrLineCkt.AcTransmissionLineId = acTrLine.AcTransmissionLineId;
                newAcTrLineCkt.CktNumber = acTrLineCktForeign.CktNumber.ToString();
                newAcTrLineCkt.CODDate = acTrLineCktForeign.CODDate;
                newAcTrLineCkt.CommDate = acTrLineCktForeign.CommDate;
                newAcTrLineCkt.DeCommDate = acTrLineCktForeign.DeCommDate;
                newAcTrLineCkt.FtcDate = acTrLineCktForeign.FtcDate;
                newAcTrLineCkt.Length = acTrLineCktForeign.Length;
                newAcTrLineCkt.SIL = acTrLineCktForeign.SIL;
                newAcTrLineCkt.ThermalLimitMVA = acTrLineCktForeign.ThermalLimitMVA;
                newAcTrLineCkt.TrialOperationDate = acTrLineCktForeign.TrialOperationDate;
                newAcTrLineCkt.WebUatId = acTrLineCktForeign.WebUatId;

                _context.AcTransLineCkts.Add(newAcTrLineCkt);
                await _context.SaveChangesAsync();
                return newAcTrLineCkt;
            }

            // check if we have to modify the entity
            if (opt == EntityWriteOption.Modify && existingAcTrLineCkt != null)
            {
                existingAcTrLineCkt.Name = acTrLineCktForeign.Name;
                existingAcTrLineCkt.AcTransmissionLineId = acTrLine.AcTransmissionLineId;
                existingAcTrLineCkt.CktNumber = acTrLineCktForeign.CktNumber.ToString();
                existingAcTrLineCkt.CODDate = acTrLineCktForeign.CODDate;
                existingAcTrLineCkt.CommDate = acTrLineCktForeign.CommDate;
                existingAcTrLineCkt.DeCommDate = acTrLineCktForeign.DeCommDate;
                existingAcTrLineCkt.FtcDate = acTrLineCktForeign.FtcDate;
                existingAcTrLineCkt.Length = acTrLineCktForeign.Length;
                existingAcTrLineCkt.SIL = acTrLineCktForeign.SIL;
                existingAcTrLineCkt.ThermalLimitMVA = acTrLineCktForeign.ThermalLimitMVA;
                existingAcTrLineCkt.TrialOperationDate = acTrLineCktForeign.TrialOperationDate;
                await _context.SaveChangesAsync();
                return existingAcTrLineCkt;
            }
            return null;
        }
    }
}
