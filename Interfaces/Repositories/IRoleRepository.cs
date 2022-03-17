using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IRoleRepository
    {
         public Task<Role> AddRoleAsync(Role role);
         
         public Task<Role> GetRoleByNameAsync(string Name);

         Task<Role> UpdateRoleAsync(int id, Role role);

         Task<Role> GetRoleById(int id);

         Task<Role> DeleteRoleAsyn(int id);

         Task<IEnumerable<Role>> GetAllRoles();
    }
}