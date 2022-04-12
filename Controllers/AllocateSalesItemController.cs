using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Implementations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class AllocateSalesItem:Controller
    {
        private readonly AllocateSalesItemToSalesManagerService _allocateSalesItemToSalesManager;
        private readonly ItemService _itemService;
        private readonly SalesManagerService _salesManagerService;
        private readonly StockKeeperService _stockKeeperService;
        private readonly StockService _stockService;

        public AllocateSalesItem(AllocateSalesItemToSalesManagerService allocateSalesItemToSalesManager, 
            ItemService itemService, SalesManagerService salesManagerService, 
            StockKeeperService stockKeeperService, StockService stockService)
        {
            _allocateSalesItemToSalesManager = allocateSalesItemToSalesManager;
            _itemService = itemService;
            _salesManagerService = salesManagerService;
            _stockKeeperService = stockKeeperService;
            _stockService = stockService;
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper, SalesManager")]
        public async Task<IActionResult> Index()
        {
            return View(await _allocateSalesItemToSalesManager.GetAllAllocatedSalesItem());
        }
        
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> AllocateItemsToSalesStore()
        {
            var stockKeepers = await _stockKeeperService.GetAllStockKeepers();
            var salesManagers = await _salesManagerService.GetAllSalesManagers();
            //var stocks = await _stockService.GetAllStockItems();
            var items = await _itemService.GetAllItems();
            ViewData["Items"] = new SelectList(items, "Id", "ItemName");
            //ViewData["StockItems"] = new SelectList(stocks, "Id", "StockName");
            ViewData["StockKeepers"] = new SelectList(stockKeepers, "Id", "${FirstName} {LastName}");
            ViewData["SalesManagers"] = new SelectList(salesManagers, "Id", "{FirstName} {LastName}");
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> AllocateItemsToSalesStore(CreateAllocationResponseModel model)
        {
            var allocateItems = await _allocateSalesItemToSalesManager.AllocateSalesItem(model);
            var stockItem = await _stockService.GetStockItemById(model.StockItemId);
            string alertMessage =
                $"Insufficient Stock! The remaining quantity for the selected item is {stockItem.Quantity}";
            if (allocateItems==null)
            {
                ViewBag.error = alertMessage;
            }

            TempData["data"] = "Item successfully allocated";
            return RedirectToAction("Index");
        }
    }
}