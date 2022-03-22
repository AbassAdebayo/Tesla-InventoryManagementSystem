using System;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [Authorize(Roles = "ShopManager, StockKeeper")]
        public async Task<IActionResult> Index()
        {
            return View(await _supplierService.GetAllSuppliers());
        }

        [HttpGet]
        public IActionResult RegisterSupplier()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> RegisterSupplier(CreateSupplierRequestModel model)
        {
            await _supplierService.CreateSupplier(model);
            return RedirectToAction("Index");


        }
        
        
        [HttpGet]
        [Authorize(Roles = "ShopManager, StockKeeper")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);

            if (supplier==null)
            {
                throw new Exception("Supplier not found!");
            }
            return View(supplier);
        }

        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> UpdateSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier==null)
            {
                throw new Exception("Operation not successful!");
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateSupplier(int id, UpdateSupplierRequestModel model)
        {
            await _supplierService.UpdateSupplier(id, model);
            return RedirectToAction("Index");

        }
        
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);

            if (supplier==null)
            {
                throw new Exception("Supplier not found!");
            }
            return View(supplier);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            await _supplierService.DeleteSupplier(id);
            return RedirectToAction("Index");

        }
        
    }
}