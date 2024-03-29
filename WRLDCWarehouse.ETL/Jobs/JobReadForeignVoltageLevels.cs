﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.Core.Entities;
using WRLDCWarehouse.ETL.Extracts;
using WRLDCWarehouse.ETL.Loads;
using WRLDCWarehouse.ETL.Enums;
using Microsoft.Extensions.Logging;

namespace WRLDCWarehouse.ETL.Jobs
{
    public class JobReadForeignVoltageLevels
    {
        public async Task ImportForeignVoltageLevels(WRLDCWarehouseDbContext _context, ILogger _log, string oracleConnStr, EntityWriteOption opt)
        {
            VoltLevelExtract voltLevelExtract = new VoltLevelExtract();
            List<VoltLevel> voltLevels = voltLevelExtract.ExtractVoltageLevels(oracleConnStr);

            LoadVoltageLevel loadVoltageLevel = new LoadVoltageLevel();
            foreach (VoltLevel voltLevel in voltLevels)
            {
                VoltLevel insertedVolt = await loadVoltageLevel.LoadSingleAsync(_context, _log, voltLevel, opt);
            }
        }
    }
}
