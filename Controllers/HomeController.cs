using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryManagemenSystem_Ims.Models;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;

        public HomeController(ILogger<HomeController> logger, ISalesService salesService, 
        ICustomerService customerService)
        {
            _logger = logger;
            _salesService = salesService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
       
        
        		
    }
}