using InventoryManagemenSystem_Ims.Interfaces.Services;
using InventoryManagemenSystem_Ims.SendMail;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class SalesManagerController:Controller
    {
        private readonly ISalesManagerService _salesManagerService;
        private readonly IReportService _reportService;
        private readonly IMailMessage _mailMessage;

        public SalesManagerController(ISalesManagerService salesManagerService, IReportService reportService, IMailMessage mailMessage)
        {
            _salesManagerService = salesManagerService;
            _reportService = reportService;
            _mailMessage = mailMessage;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        }
    }
}