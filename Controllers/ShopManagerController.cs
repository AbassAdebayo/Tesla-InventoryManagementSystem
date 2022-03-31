using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class ShopManagerController : Controller
    {
        private readonly IShopManagerService _shopManagerService;
        private readonly IReportService _reportService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        


        public ShopManagerController(IShopManagerService shopManagerService, IReportService reportService,
            IRoleService roleService, IUserService userService)
        {
            _shopManagerService = shopManagerService;
            _reportService = reportService;
            _roleService = roleService;
            _userService = userService;
            
        }
      
        
        
        [HttpGet]
        public IActionResult Index()
        {
            //var shopManagers = _shopManagerService.GetAllShopManagers();
            return View();
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var roles = await _roleService.GetAllRoles();
            ViewData["Roles"] = new SelectList(roles, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterShopManagerRequestModel model)
        {
            await _shopManagerService.RegisterShopManager(model);
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        [Authorize]
        public IActionResult Update()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shopManager = _shopManagerService.GetShopManagerById(id);
            if (shopManager == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] int id,
            [FromBody] UpdateShopManagerRequestModel model)
        {
            await _shopManagerService.UpdateShopManager(id, model);
            return RedirectToAction("Profile");

        }
        
        [HttpGet]
        public IActionResult DeleteShopManager(int id)
        {
            var shopManager = _shopManagerService.GetShopManagerById(id);
            if (shopManager == null)
            {
                return NotFound();
            }

            return View();
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _shopManagerService.DeleteShopManager(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Display(Name = "Delete Sales Manager report")]
        public async Task<IActionResult> DeleteSalesManagerReportConfirmed(int id)
        {
            await _reportService.DeleteSalesManagerReport(id);
            return Ok();
        }


        
        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {

            var shopManager = await _shopManagerService.GetShopManagerById(id);
            return View(new BaseResponse<ShopManagerDto>
            {
                Data = shopManager.Data
            });
            // if (user == null)
            // {
            //     return RedirectToAction("Login");
            // }

            
        }

        
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }
        
        
        [HttpGet]
        public IActionResult Delete(string userName)
        {

            var user = _userService.GetUserByUserName(userName);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed([FromRoute]string userName)
        {
            _userService.DeleteUser(userName);
            return RedirectToAction("GetUsers");
        }
        
        [HttpGet]
        //[Authorize]
        public IActionResult UpdateUser()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, UpdateUserRequestModel model)
        {
            await _userService.UpdateUser(id, model);
            return RedirectToAction("Profile");
        }
        
        
        [HttpPost]
        [Display(Name = "Delete Stock Keeper report")]
        //[Authorize]
        public async Task<IActionResult> DeleteStockKeeperReportConfirmed(int id)
        {
            await _reportService.DeleteStockKeeperReport(id);
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> StockKeeperReportIndex()
        {
            var stockKeeperReports = await _reportService.GetAllStockKeeperReports();
            return Ok(new BaseResponse<IList<ReportDto>>
            {
                Message = "Data fetched successfully", 
                Status = true,
                Data = stockKeeperReports
            });
        }
        
       
        // public async Task<IActionResult> SalesManagerReportIndex()
        // {
        //     var stockKeeperReports = await _reportService.GetAllSalesManagerReports();
        //     return Ok(new BaseResponse<IList<ReportDto>>
        //     {
        //         Message = "Data fetched successfully", 
        //         Status = true,
        //         Data = stockKeeperReports
        //     });
        // }
        
        [HttpPost]
        public async Task<IActionResult> UpdateReportToVerified(int id)
        {
            await _reportService.UpdateReportStatusToVerified(id);
            return RedirectToAction("ViewVerifiedReports");

        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateReportToApproved(int id)
        {
            await _reportService.UpdateReportStatusToApproved(id);
            return RedirectToAction("ViewApprovedReports");

        }
        
        [HttpGet]
        public async Task<IActionResult> ViewVerifiedReports()
        {
            var verifiedReports = await _reportService.ViewStockKeeperVerifiedReports();
            return View(verifiedReports);
        }
        
        [HttpGet]
        public async Task<IActionResult> ViewStockKeeperApprovedReports()
        {
            var approvedReports = await _reportService.ViewStockKeeperApprovedReports();
            return View(approvedReports);
        }

        

        [HttpGet]
        public async Task<IActionResult> ViewSalesVerifiedReports()
        {
            var verifiedReports = await _reportService.ViewSalesManagerVerifiedReports();
            return View(verifiedReports);
        }
        

        [HttpGet]
        public async Task<IActionResult> ViewApprovedReports()
        {
            var approvedReports = await _reportService.ViewSalesManagerApprovedReports();
            return View(approvedReports);
        }
    }
    
}