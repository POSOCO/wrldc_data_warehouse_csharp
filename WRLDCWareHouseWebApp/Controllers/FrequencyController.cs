using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WRLDCWarehouse.Data;
using WRLDCWarehouse.ETL.Jobs;
using WRLDCWareHouseWebApp.ViewModels;

namespace WRLDCWareHouseWebApp.Controllers
{
    public class FrequencyController : Controller
    {
        private readonly WRLDCWarehouseDbContext _context;

        public FrequencyController(WRLDCWarehouseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Frequency/TransformFrequency
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransformFrequency([Bind] FreqPageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(vm);
        }
    }
}