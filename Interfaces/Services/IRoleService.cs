using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IRoleService
    {
        public Task<Role> AddRoleAsync(CreateRoleRequestModel model);
         
        public Task<Role> GetRoleByNameAsync(string Name);

        Task<Role> UpdateRoleAsync(int id, UpdateRoleRequestModel model);

        Task<Role> DeleteRoleAsync(int id);
        Task<IEnumerable<Role>> GetAllRoles();
    }
}