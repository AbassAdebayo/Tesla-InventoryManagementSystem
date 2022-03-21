using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Auth;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using InventoryManagemenSystem_Ims.SendMail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class StockKeeperController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IStockKeeperService _stockKeeperService;
        private readonly IReportService _reportService;
        private readonly IMailMessage _mailMessage;
        private readonly IRoleService _roleService;
      
        

        public StockKeeperController(IStockService stockService, IStockKeeperService stockKeeperService, IReportService reportService, 
            IMailMessage mailMessage, IRoleService roleService)
        {
            _stockService = stockService;
            _stockKeeperService = stockKeeperService;
            _reportService = reportService;
            _mailMessage = mailMessage;
            _roleService = roleService;
            
            
        }
        
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> GetAllStockKeepers()
        {
            var stockKeepers = await _stockKeeperService.GetAllStockKeepers();
            return View(stockKeepers);
        }
        
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> RegisterStockKeeper()
        {
            var roles = await _roleService.GetAllRoles();
            ViewData["Roles"] = new SelectList(roles, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterStockKeeper(RegisterStockKeeperRequestModel model)
        {
            await _stockKeeperService.RegisterStockKeeper(model);
            return RedirectToAction("Index", "User");
        }



        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public IActionResult UpdateStockKeeper()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var stockKeeper = _stockKeeperService.GetStockKeeperById(id);
            if (stockKeeper == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStockKeeper([FromRoute] int id,
            [FromBody] UpdateStockKeeperRequestModel model)
        {
            await _stockKeeperService.UpdateStockKeeper(id, model);
            return RedirectToAction("Index", "User");

        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public IActionResult DeleteStockKeeper(int id)
        {
            var stockKeeper = _stockKeeperService.GetStockKeeperById(id);
            if (stockKeeper == null)
            {
                return NotFound();
            }

            return View();
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _stockKeeperService.DeleteStockKeeper(id);
            return RedirectToAction("Index", "User");
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager")]
        public async Task<IActionResult> StockKeeperProfile(int id)
        {
            var salesManager = await _stockKeeperService.GetStockKeeperById(id);
            return View(salesManager);
            


        }
    }


}