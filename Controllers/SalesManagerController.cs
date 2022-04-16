using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Auth;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using InventoryManagemenSystem_Ims.SendMail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using MimeKit;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class SalesManagerController : Controller
    {
        private readonly ISalesManagerService _salesManagerService;
        private readonly IMailMessage _mailMessage;
        private readonly IRoleService _roleService;
        
        

        public SalesManagerController(ISalesManagerService salesManagerService, IMailMessage mailMessage, IRoleService roleService)
        {
            _salesManagerService = salesManagerService;
            _mailMessage = mailMessage;
            _roleService = roleService;
           
            
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> GetAllSalesManagers()
        {
            var salesManager = await _salesManagerService.GetAllSalesManagers();
            return View(salesManager);
        }

        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> RegisterSalesManager()
        {
            var roles = await _roleService.GetAllRoles();
            ViewData["Roles"] = new SelectList(roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSalesManager(RegisterSalesManagerRequestModel model)
        {
            await _salesManagerService.RegisterSalesManager(model);
            return RedirectToAction("Index", "User");
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public IActionResult UpdateSalesManager()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shopManager = _salesManagerService.GetSalesManagerById(id);
            if (shopManager == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> UpdateSalesManager([FromRoute] int id,
            [FromBody] UpdateSalesManagerRequestModel model)
        {
            await _salesManagerService.UpdateSalesManager(id, model);
            return RedirectToAction("SalesManagerProfile");

        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public IActionResult DeleteSalesManager(int id)
        {
            var salesManager = _salesManagerService.GetSalesManagerById(id);
            if (salesManager == null)
            {
                return NotFound();
            }

            return View();
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _salesManagerService.DeleteSalesManager(id);
            return RedirectToAction("GetAllSalesManagers");
        }
        
       
       
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> SalesManagerProfile(int id)
        {
            //var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var salesManager = await _salesManagerService.GetSalesManagerById(id);
            return View(salesManager);
            


        }
        
        
        
    }
}