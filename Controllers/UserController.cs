using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Annotations;
using InventoryManagemenSystem_Ims.Auth;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserService _userService;
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;
        private readonly INotificationService _notificationService;
        private readonly IStockService _stockService;


        public UserController(IUserService userService, ISalesService salesService, 
            ICustomerService customerService, INotificationService notificationService, IStockService stockService)
        {
            _userService = userService;
            _salesService = salesService;
            _customerService = customerService;
            _notificationService = notificationService;
            _stockService = stockService;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var roleAdd = "";
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
                    roleAdd = role.Name;
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                TempData["data"] = "Login successfully!";
                TempData["Login"] = response.Status;

                if (roleAdd=="ShopManager")
                {
                    return RedirectToAction("Index");
                }

                if (roleAdd=="StockKeeper")
                {
                    return RedirectToAction("StockKeeperIndex");
                }
                if (roleAdd=="SalesManager")
                {
                    return RedirectToAction("SalesManagerIndex");
                }
                
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
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> Index()
        {
            // var sales = await _salesService.GetAllSales();
            // var customers = await _customerService.GetAllCustomers();
            // var salesEnumerable = sales as Sales[] ?? sales.ToArray();
            // var numOfSales = salesEnumerable.Count();
            // List<int> salesItem = new List<int>();
            // List<int> customerNumbersById = new List<int>();
            // var numbOfCustomer = customers.Count();
            //
            // foreach(var item in salesEnumerable)
            // {
            //     salesItem.Add(item.ItemId);
            //     customerNumbersById.Add(item.CustomerId);
            // }
            // var sItem = salesItem;
            // var customerNum = customerNumbersById;
            //
            // ViewBag.SALESITEM = sItem;
            // ViewBag.CUSTOMERNUMBERS = customerNum;
            // ViewBag.CUSTOMERS = customers;
            // ViewBag.SALES = numOfSales;
            
            var overallSales = await _salesService.GetGrandTotalOfAllSales();
            var notifications = await _notificationService.GetNewNotifications();
            var expenses = await _stockService.GetExpenses();

            ViewBag.SALESGRANDTOTAL = overallSales.Data;
            ViewBag.NOTIFICATIONSCOUNT = notifications.Count;
            ViewBag.EXPENSES = expenses;
            int ChangeInNotification = 0;
            
            int currentCount = notifications.Count;
            if (currentCount>ChangeInNotification)
            {
                ChangeInNotification = currentCount;
                ViewBag.Message = $"You have {ChangeInNotification} notification(s)";
            }

            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper")]
        public async Task<IActionResult> StockKeeperIndex()
        {
            
            var overallSales = _salesService.GetGrandTotalOfAllSales();
            var notifications = await _notificationService.GetNewNotifications();
            ViewBag.SALESGRANDTOTAL = overallSales.Result.Data;
            
            int ChangeInNotification = 0;
            
            int currentCount = notifications.Count;
            if (currentCount>ChangeInNotification)
            {
                ChangeInNotification = currentCount;
                ViewBag.Message = $"You have {ChangeInNotification} notification(s)";
            }
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles =  "SalesManager")]
        [ItemCanBeNull]
        public async Task<IActionResult> SalesManagerIndex()
        {
            
            var overallSales = _salesService.GetGrandTotalOfAllSales();

            ViewBag.SALESGRANDTOTAL = overallSales.Result.Data;
            int ChangeInNotification = 0;
            var notifications = await _notificationService.GetNewNotifications();
            int currentCount = notifications.Count;
            if (currentCount>ChangeInNotification)
            {
                ChangeInNotification = currentCount;
                ViewBag.Message = $"You have {ChangeInNotification} notification(s)";
            }
            return View();
            
        }
       
        [HttpPost]
        [Authorize(Roles =  "ShopManager, SalesManager")]
        public IActionResult GetSalesByDate(DateTime dateCreated)
        {
            
            var salesByDate = _salesService.GetSalesByMonth(dateCreated);
            return View(salesByDate);
        }
        
        [HttpPost]
        [Authorize(Roles =  "ShopManager, StockKeeper")]
        public async Task<IActionResult> GetStockItemByDate(DateTime dateCreated)
        {
            
            var getStockItemByDate = await _stockService.GetStockItemByDate(dateCreated);
            return View(getStockItemByDate);
        }
        
        
        
    }
}