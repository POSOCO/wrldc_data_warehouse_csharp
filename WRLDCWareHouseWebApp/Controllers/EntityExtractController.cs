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
                await job.ImportForeignVoltageLevels(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Extracting Voltage Levels";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/Regions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Regions([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignRegions job = new JobReadForeignRegions();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignRegions(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Extracting Regions";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/Owners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Owners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignOwners job = new JobReadForeignOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignOwners(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Extracting Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/ConductorTypes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConductorTypes([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignConductorTypes job = new JobReadForeignConductorTypes();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignConductorTypes(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Extracting Conductor types";
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}