using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.ETL.Jobs;
using WRLDCWareHouseWebApp.ViewModels;

namespace WRLDCWareHouseWebApp.Controllers
{
    public class EntityExtractController : Controller
    {
        private readonly WRLDCWarehouseDbContext _context;
        public IConfiguration Configuration { get; }

        public EntityExtractController(WRLDCWarehouseDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: EntityExtract/VoltLevels
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VoltLevels([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignVoltageLevels job = new JobReadForeignVoltageLevels();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ExtractForeignVoltageLevels(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Extracting Voltage Levels";
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}