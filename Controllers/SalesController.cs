using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly IItemService _itemService;
        private readonly IAllocateSalesItemToSalesManagerService _allocateSalesItemToSalesManager;
        private readonly ICustomerService _customerService;
        private readonly ISalesManagerService _salesManagerService;


        public SalesController(ISalesService salesService, IItemService itemService, IAllocateSalesItemToSalesManagerService allocateSalesItemToSalesManager, ICustomerService customerService, ISalesManagerService salesManagerService)
        {
            _salesService = salesService;
            _itemService = itemService;
            _allocateSalesItemToSalesManager = allocateSalesItemToSalesManager;
            _customerService = customerService;
            _salesManagerService = salesManagerService;
        }

        [HttpGet]
        [Authorize(Roles = "SalesManager, ShopManager")]
        public async Task<IActionResult> Index()
        {

            return View(await _salesService.GetAllSales());
        }

        [HttpGet]
        [Authorize(Roles = "SalesManager")]
        public async Task<IActionResult> StartSales()
        {
            var customers = await _customerService.GetAllCustomers();
            var salesManagers = await _salesManagerService.GetAllSalesManagers();
            var items = await _itemService.GetAllItems();
            ViewData["Items"] = new SelectList(items, "Id", "ItemName");
            ViewData["Customers"] = new SelectList(customers, "Id", "CompanyName");
            ViewData["SalesManagers"] = new SelectList(salesManagers, "Id", "FirstName");

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> StartSales(CreateSalesRequestModel model)
        {
            var sales = await _salesService.StartSales(model);
            string alertMessage = "The quantity available cannot meet the request!";

            if (sales==null)
            {
                ViewBag.error = alertMessage;
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Authorize(Roles = "ShopManager,SalesManager")]
        public async Task<IActionResult> ReturnGoods()
        {
            var customers = await _customerService.GetAllCustomers();
            var salesManagers = await _salesManagerService.GetAllSalesManagers();
            // var items = await _itemService.GetAllItems();
            // ViewData["Items"] = new SelectList(items, "Id", "ItemName");
            ViewData["Customers"] = new SelectList(customers, "Id", "CompanyName");
            ViewData["SalesManagers"] = new SelectList(salesManagers, "Id", "FirstName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReturnGoods(ReturnGoodsRequestModel model, int id)
        {
           string errorMessage = "The time interval for returning goods has elapsed";
           
           
            var check = await _salesService.ReturnGoods(model, id);

            if (check.Status == true)
            {
                return RedirectToAction("SalesManagerIndex","User");
            }
            
            ViewBag.error = errorMessage;
            return View();





        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager")]
        public async Task <IActionResult> GenerateInvoice(int id)
        {
            return View(await _salesService.GenerateInvoice(id));
        }
        

       
    }
}