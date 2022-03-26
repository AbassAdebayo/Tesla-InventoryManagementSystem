using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class SalesItem:Controller
    {
        private readonly ISalesItemService _salesItemService;
        private readonly ICustomerService _customerService;
        private readonly ISalesManagerService _salesManagerService;

        public SalesItem(ISalesItemService salesItemService, ICustomerService customerService, ISalesManagerService salesManagerService)
        {
            _salesItemService = salesItemService;
            _customerService = customerService;
            _salesManagerService = salesManagerService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _salesItemService.GetAllSalesItems());

        }
       
        
       
    }
}