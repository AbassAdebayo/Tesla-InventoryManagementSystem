using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Auth;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserService _userService;
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;
        
        public UserController(IUserService userService, ISalesService salesService, ICustomerService customerService)
        {
            _userService = userService;
            _salesService = salesService;
            _customerService = customerService;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
           
            var response = await _userService.Login(model);
            
            if (response.Status==true)
            {
                var claims = new List<Claim>
                {
                  
                    new Claim(ClaimTypes.NameIdentifier, response.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name, response.Data.Email)
                };

                foreach (var role in response.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                TempData["data"] = "Login successfully!";
                TempData["Login"] = response.Status;
                
                return RedirectToAction("Index");
            }

            ViewBag.error = "Invalid Email or Password";
            return View();



        }
        
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            TempData["Data"] = true;
            return RedirectToAction("Login");
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager, SalesManager, StockKeeper")]
        public async Task<IActionResult> Index()
        {
            var sales = await _salesService.GetAllSales();
            var customers = await _customerService.GetAllCustomers();
            var salesEnumerable = sales as Sales[] ?? sales.ToArray();
            var numOfSales = salesEnumerable.Count();
            List<int> salesItem = new List<int>();
            List<int> customerNumbersById = new List<int>();
            var numbOfCustomer = customers.Count();

            foreach(var item in salesEnumerable)
            {
                salesItem.Add(item.ItemId);
                customerNumbersById.Add(item.CustomerId);
            }
            var sItem = salesItem;
            var customerNum = customerNumbersById;

            ViewBag.SALESITEM = sItem;
            ViewBag.CUSTOMERNUMBERS = customerNum;
            ViewBag.CUSTOMERS = customers;
            ViewBag.SALES = numOfSales;
            return View();
        }
        
       
      
    }
}