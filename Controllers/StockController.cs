using System;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class StockController:Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper")]
        public IActionResult Index()
        {
            return View();
        }
        
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public IActionResult CreateStock()
        {
            return View();
        }
        
        
        [HttpPost] 
        public async Task<IActionResult> CreateStock(CreateStockRequestModel model)
        {
            
            await _stockService.CreateStock(model);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStock(int id)
        {
            var stock = await _stockService.GetStockById(id);
            return Ok(stock);
        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> UpdateStock(int id)
        {
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
    }
}