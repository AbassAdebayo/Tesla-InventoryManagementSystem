using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class RoleController:Controller
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRoles();
            return View(roles);
        }
        
        
        public IActionResult CreateRole()
        {
            return View();

        }
        
        
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleRequestModel model)
        {
            await _roleService.AddRoleAsync(model);
            return RedirectToAction("Index");

        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string roleName)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName);
            return View(role);

        }
        
        [HttpGet]
        public async Task<IActionResult> Update(string roleName)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateRole(string roleName, UpdateRoleRequestModel model)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName);
            await _roleService.UpdateRoleAsync(role.Id, model);
            return RedirectToAction("Index");

        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(string roleName)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _roleService.DeleteRoleAsync(id);
            return RedirectToAction("Index");
        }
    }
}