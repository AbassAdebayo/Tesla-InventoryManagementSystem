using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class Category:Controller
    {
        private readonly ICategoryService _categoryService;

        public Category(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [Authorize(Roles = "ShopManager, StockKeeper, SalesManager")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }
        
        [Authorize(Roles = "StockKeeper")]
        public IActionResult CreateItemCategory()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateItemCategory(CreateCategoryRequestModel model)
        {
            _categoryService.CreateCategory(model);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager, StockKeeper, SalesManager")]
        public async Task<IActionResult> GetItemCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return Ok(category);
        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> UpdateItemCategory(int id)
        {
            var itemCategory = await _categoryService.GetCategoryById(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return View();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> UpdateItemCategory(int id, UpdateCategoryRequestModel model)
        {
            await _categoryService.UpdateCategory(id, model);
            return RedirectToAction("Index");

        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> DeleteItemCategory(int id)
        {
            var itemCategory = await _categoryService.GetCategoryById(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return View(itemCategory);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}