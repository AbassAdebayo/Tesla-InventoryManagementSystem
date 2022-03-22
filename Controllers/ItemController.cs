using System;
using System.Collections;
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
    public class ItemController:Controller
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;

        public ItemController(IItemService itemService, ICategoryService categoryService)
        {
            _itemService = itemService;
            _categoryService = categoryService;
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper, SalesManager")]
        public async Task<IActionResult> Index()
        {
            return View(await _itemService.GetAllItems());
        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> AddItem()
        {
            var categories =  await _categoryService.GetAllCategories();
            ViewData["Categories"] = new SelectList(categories, "Id", "CategoryName");
            
           return View();
           

        }
        
        [HttpPost] 
        public async Task<IActionResult> AddItem(CreateItemRequestModel model)
        {
            
            await _itemService.CreateItem(model);
            return RedirectToAction("Index");


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
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> EditItem(int id)
        {

            var item = await _itemService.GetItemById(id);
            if (item==null)
            {
                throw new Exception("Request unsuccessful");
            }

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> EditItem(int id, UpdateItemRequestModel model)
        {

            await _itemService.UpdateItem(id, model);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _itemService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _itemService.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}