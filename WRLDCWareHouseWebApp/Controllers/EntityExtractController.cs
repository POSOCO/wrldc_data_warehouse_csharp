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
                TempData["Message"] = "Completed Importing Voltage Levels";
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
                TempData["Message"] = "Completed Importing Regions";
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
                TempData["Message"] = "Completed Importing Owners";
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
                TempData["Message"] = "Completed Importing Conductor types";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/States
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> States([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignStates job = new JobReadForeignStates();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignStates(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing States";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/MajorSubstations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MajorSubstations([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignMajorSubstations job = new JobReadForeignMajorSubstations();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignMajorSubstations(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Major Substations";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/Substations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Substations([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignSubstations job = new JobReadForeignSubstations();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignSubstations(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Substations";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/SubstationOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubstationOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignSubstationOwners job = new JobReadForeignSubstationOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignSubstationOwners(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Substation Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/AcTransmissionLines
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcTransmissionLines([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignAcTransLines job = new JobReadForeignAcTransLines();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignAcTransLines(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing AcTransmission Lines";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/AcTransLineCkts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcTransLineCkts([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignAcTransLineCkts job = new JobReadForeignAcTransLineCkts();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignAcTransLineCkts(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                await job.ImportForeignAcTransLineCktCondTypes(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Ac Transmission Line Circuits along with conductor types";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/AcTransLineCktOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcTransLineCktOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignAcTransLineCktOwners job = new JobReadForeignAcTransLineCktOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignAcTransLineCktOwners(_context, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Ac Transmission Line Circuits Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}