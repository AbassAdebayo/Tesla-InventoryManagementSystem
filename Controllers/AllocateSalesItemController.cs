using System;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Implementations.Services;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class AllocateSalesItemController:Controller
    {
         private readonly IAllocateSalesItemToSalesManagerService _allocateSalesItemToSalesManager;
        private readonly IItemService _itemService;
        private readonly ISalesManagerService _salesManagerService;
        private readonly IStockKeeperService _stockKeeperService;
        private readonly IStockService _stockService;
        private readonly INotificationService _notificationService;

        public AllocateSalesItemController(IAllocateSalesItemToSalesManagerService allocateSalesItemToSalesManager, IItemService itemService, ISalesManagerService salesManagerService, IStockKeeperService stockKeeperService, IStockService stockService, INotificationService notificationService)
        {
            _allocateSalesItemToSalesManager = allocateSalesItemToSalesManager;
            _itemService = itemService;
            _salesManagerService = salesManagerService;
            _stockKeeperService = stockKeeperService;
            _stockService = stockService;
            _notificationService = notificationService;
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper, SalesManager")]
        public async Task<IActionResult> Index()
        {
            return View(await _allocateSalesItemToSalesManager.GetAllAllocatedSalesItem());
        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager, ShopManager, StockKeeper")]
        public async Task<IActionResult> GetAllocationDetails(int id)
        {
            var allocationDetails = await _allocateSalesItemToSalesManager.GetAllocatedSalesItem(id);

            if (allocationDetails==null)
            {
                throw new Exception("Allocation not found!");
            }
            return View(allocationDetails);
        }
        
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> AllocateItemsToSalesStore()
        {
            var stockKeepers = await _stockKeeperService.GetAllStockKeepers();
            var salesManagers = await _salesManagerService.GetAllSalesManagers();
            //var stocksItems = await _stockService.GetAllStockItems();
            var items = await _itemService.GetAllItems();
            ViewData["Items"] = new SelectList(items, "Id", "ItemName");
            //ViewData["StockItems"] = new SelectList(stocksItems, "Id", "StockName");
            ViewData["StockKeepers"] = new SelectList(stockKeepers, "Id", "LastName");
            ViewData["SalesManagers"] = new SelectList(salesManagers, "Id", "LastName");
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> AllocateItemsToSalesStore(CreateAllocationResponseModel model)
        {
            var allocateItems = await _allocateSalesItemToSalesManager.AllocateSalesItem(model);
            var stockItem = await _stockService.GetStockItemById(model.StockItemId);

            var notifications = await _notificationService.GetAllNotifications();
            int currentCount = notifications.Count;
            int ChangeInNotification = 0;

            if (currentCount>ChangeInNotification)
            {
                ChangeInNotification = currentCount;
                ViewBag.Message = $"You have {ChangeInNotification} notification(s)";
            }
            string alertMessage =
                $"The remaining quantity for the selected item is {stockItem.Quantity}";
            if (allocateItems.QuantityAllocated>stockItem.Quantity)
            {
               TempData["Insufficient"] = alertMessage;
            }

            TempData["data"] = "Item successfully allocated";
            return RedirectToAction("Index");
        }
    }
}