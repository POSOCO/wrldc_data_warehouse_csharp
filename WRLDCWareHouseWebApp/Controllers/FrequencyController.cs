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
    public class FrequencyController : Controller
    {
        private readonly WRLDCWarehouseDbContext _context;
        private readonly ILogger<FrequencyController> _log;
        public IConfiguration Configuration { get; }

        public FrequencyController(WRLDCWarehouseDbContext context, IConfiguration configuration, ILogger<FrequencyController> log)
        {
            _context = context;
            Configuration = configuration;
            _log = log;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Frequency/TransformFrequency
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransformFrequency([Bind("FromDateValue, ToDateValue")] FreqPageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                JobTransformRawFrequency job = new JobTransformRawFrequency();
                // await job.CreateFrequencySummaryForDate(_context, vm.FromDateValue);
                string oracleConnStr = Configuration.GetConnectionString("OracleUATConnection");
                await job.ProcessFrequenciesForDates(_context, oracleConnStr, vm.FromDateValue, vm.ToDateValue);
                TempData["Message"] = "Completed Processing frequencies";
                return View("Index");
            }
            return View(vm);
        }
    }
}