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
        private readonly IReportService _reportService;
        private readonly IMailMessage _mailMessage;
        private readonly IRoleService _roleService;
        
        

        public SalesManagerController(ISalesManagerService salesManagerService, IReportService reportService, IMailMessage mailMessage, IRoleService roleService)
        {
            _salesManagerService = salesManagerService;
            _reportService = reportService;
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
        
        [HttpGet]
        [Authorize(Roles = "StockKeeper, SalesManager")]
        public IActionResult SubmitReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReport(CreateSalesManagerReportModel model, int id)
        {
            string successMessage = "Message sent successfully!";
            await _reportService.SubmitSalesManagerReport(model, id);
            TempData["data"] = successMessage;
            return RedirectToAction("ViewSalesReports");
            
            
           
        }

        [Authorize(Roles = "ShopManager, SalesManager, StockKeeper")]
        public async Task<IActionResult> ViewSalesReports()
        {
            return View(await _reportService.GetAllSalesManagerReports());
        }
        
        
    }
}