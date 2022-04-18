using System;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class StockController:Controller
    {
        private readonly IStockService _stockService;
        private readonly ISupplierService _supplierService;
        private readonly IItemService _itemService;

        public StockController(IStockService stockService, ISupplierService supplierService, IItemService itemService)
        {
            _stockService = stockService;
            _supplierService = supplierService;
            _itemService = itemService;
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper")]
        public async Task<IActionResult> Index()
        {
            return View(await _stockService.GetAllStocks());
        }
        
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> CreateStock()
        {
            var suppliers =  await _supplierService.GetAllSuppliers();
            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "CompanyName");
            
            return View();
        }
        
        
        [HttpPost] 
        public async Task<IActionResult> CreateStock(CreateStockRequestModel model)
        {
            
            await _stockService.CreateStock(model);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager, StockKeeper")]
        public async Task<IActionResult> GetStock(int id)
        {
            var stock = await _stockService.GetStockById(id);
            return View(stock);
        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> UpdateStock(int id)
        {
            var suppliers =  await _supplierService.GetAllSuppliers();
            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "CompanyName");
            var stock = await _stockService.GetStockById(id);
            if (stock == null)
            {
                return NotFound();
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateStock(int id, UpdateStockRequestModel model)
        {
            await _stockService.UpdateStock(id, model);
            return RedirectToAction("Index");

        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _stockService.GetStockById(id);
            if (stock==null)
            {
                throw new Exception("Stock doesn't exist!");
            }
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _stockService.DeleteStock(id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper")]
        public async Task<IActionResult> StockItems()
        {
            return View(await _stockService.GetAllStockItems());
        }
        
        
        [Authorize(Roles = "StockKeeper")]
        [HttpGet]
        public async Task<IActionResult> AddItemToStock()
        {
            var items =  await _itemService.GetAllItems();
            var stocks = await _stockService.GetAllStocks();
            var suppliers =  await _supplierService.GetAllSuppliers();
            ViewData["Items"] = new SelectList(items, "Id", "ItemName");
            ViewData["Stocks"] = new SelectList(stocks, "Id", "StockName");
            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "CompanyName");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddItemToStock(int id, AddItemToStockRequestModel model)
        {
            await _stockService.AddItemToStock(id, model);
            return RedirectToAction("StockItems");

        }
        
        
        [HttpGet]
        [Authorize(Roles = "ShopManager, StockKeeper")]
        public async Task<IActionResult> GetStockItem(int id)
        {
            var stockItem = await _stockService.GetStockItemById(id);
            ViewBag.itemName = stockItem.Item.ItemName;
            return View(stockItem);
        }
        
        
        [Authorize(Roles = "ShopManager,StockKeeper, SalesManager")]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _itemService.GetItemById(id);
            if (item==null)
            {
                throw new Exception("Request not found!");
            }

            return View(item);
        }
    }
}