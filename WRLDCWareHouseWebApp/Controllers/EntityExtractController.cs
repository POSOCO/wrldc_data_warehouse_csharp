using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.ETL.Jobs;
using WRLDCWareHouseWebApp.ViewModels;

namespace WRLDCWareHouseWebApp.Controllers
{
    public class EntityExtractController : Controller
    {
        private readonly WRLDCWarehouseDbContext _context;
        private readonly ILogger<EntityExtractController> _log;

        public IConfiguration Configuration { get; }

        public EntityExtractController(WRLDCWarehouseDbContext context, IConfiguration configuration, ILogger<EntityExtractController> log)
        {
            _context = context;
            Configuration = configuration;
            _log = log;
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
                await job.ImportForeignVoltageLevels(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignRegions(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignConductorTypes(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignStates(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignMajorSubstations(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignSubstations(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignSubstationOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignAcTransLines(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignAcTransLineCkts(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                await job.ImportForeignAcTransLineCktCondTypes(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
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
                await job.ImportForeignAcTransLineCktOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Ac Transmission Line Circuits Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/Buses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buses([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignBuses job = new JobReadForeignBuses();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignBuses(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Buses";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/Fuels
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fuels([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignFuels job = new JobReadForeignFuels();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignFuels(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Fuels";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/GenerationTypes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerationTypes([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignGenerationTypes job = new JobReadForeignGenerationTypes();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignGenerationTypes(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Generation Types";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/GenerationTypeFuels
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerationTypeFuels([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignGenerationTypeFuels job = new JobReadForeignGenerationTypeFuels();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignGenerationTypeFuels(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Generation Type Fuels";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/GeneratorClassifications
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratorClassifications([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignGeneratorClassifications job = new JobReadForeignGeneratorClassifications();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignGenClassifications(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Generator Classifications";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/GeneratingStations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratingStations([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignGeneratingStations job = new JobReadForeignGeneratingStations();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignGeneratingStations(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Generating Stations";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/GeneratingStationOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratingStationOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignGeneratingStationOwners job = new JobReadForeignGeneratingStationOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignGeneratingStationOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Generating StationOwners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/GeneratorStages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratorStages([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignGeneratorStages job = new JobReadForeignGeneratorStages();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignGeneratorStages(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Generator Stages";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/GeneratorUnits
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratorUnits([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignGeneratorUnits job = new JobReadForeignGeneratorUnits();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignGeneratorUnits(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Generator Units";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/TransformerTypes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransformerTypes([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignTransformerTypes job = new JobReadForeignTransformerTypes();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignTransformerTypes(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Transformer Types";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/Transformers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transformers([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignTransformers job = new JobReadForeignTransformers();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignTransformers(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Transformers";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/TransformerOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransformerOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignTransformerOwners job = new JobReadForeignTransformerOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignTransformerOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing Transformer Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/BusReactors
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BusReactors([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignBusReactors job = new JobReadForeignBusReactors();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignBusReactors(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing BusReactors";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/BusReactorOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BusReactorOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignBusReactorOwners job = new JobReadForeignBusReactorOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignBusReactorOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing BusReactor Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/LineReactors
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LineReactors([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignLineReactors job = new JobReadForeignLineReactors();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignLineReactors(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing LineReactors";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/LineReactorOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LineReactorOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignLineReactorOwners job = new JobReadForeignLineReactorOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignLineReactorOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing LineReactor Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/FilterBanks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterBanks([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignFilterBanks job = new JobReadForeignFilterBanks();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignFilterBanks(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing FilterBanks";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/FilterBankOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterBankOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignFilterBankOwners job = new JobReadForeignFilterBankOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignFilterBankOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing FilterBank Owners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/HvdcLines
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HvdcLines([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignHvdcLines job = new JobReadForeignHvdcLines();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignHvdcLines(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing HvdcLines";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/HvdcLineCkts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HvdcLineCkts([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignHvdcLineCkts job = new JobReadForeignHvdcLineCkts();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignHvdcLineCkts(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing HvdcLineCkts";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/HvdcLineCktOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HvdcLineCktOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignHvdcLineCktOwners job = new JobReadForeignHvdcLineCktOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignHvdcLineCktOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing HvdcLineCktOwners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/HvdcPoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HvdcPoles([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignHvdcPoles job = new JobReadForeignHvdcPoles();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignHvdcPoles(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing HvdcPoles";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: EntityExtract/HvdcPoleOwners
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HvdcPoleOwners([Bind("EntityWriteOption")] EntityExtractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobReadForeignHvdcPoleOwners job = new JobReadForeignHvdcPoleOwners();
                string oracleWebUatConnStr = Configuration.GetConnectionString("OracleWebUIUATConnection");
                await job.ImportForeignHvdcPoleOwners(_context, _log, oracleWebUatConnStr, vm.EntityWriteOption);
                TempData["Message"] = "Completed Importing HvdcPoleOwners";
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}