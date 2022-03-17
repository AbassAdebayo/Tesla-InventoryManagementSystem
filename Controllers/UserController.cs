using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        
      
    }
}