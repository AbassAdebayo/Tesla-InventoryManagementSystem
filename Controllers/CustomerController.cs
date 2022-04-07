using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class 
    CustomerController:Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager, ShopManager")]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomers();
            return View(customers);
        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager")]
        public IActionResult RegisterCustomer()
        {
            return View();
        }
        
        [HttpPost] 
        public async Task<IActionResult> RegisterCustomer(CreateCustomerRequestModel model)
        {
            await _customerService.CreateCustomer(model);
            return RedirectToAction("Index");

        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager, ShopManager")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer==null)
            {
                throw new Exception("Customer not found!");
            }
            return View(customer);
        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager")]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer==null)
            {
                throw new Exception("Operation not successful!");
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequestModel model)
        {
            await _customerService.UpdateCustomer(id, model);
            return RedirectToAction("Index");

        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer==null)
            {
                throw new Exception("Customer not found!");
            }
            return View(customer);
        }
        
        [HttpPost]
       [Authorize(Roles = "SalesManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomer(id);
            return RedirectToAction("Index");

        }
    }
}