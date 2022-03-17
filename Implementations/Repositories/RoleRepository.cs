using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly ImsContext _imsContext;

        public RoleRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<Role> AddRoleAsync(Role role)
        {
            await _imsContext.Roles.AddAsync(role);
            await _imsContext.SaveChangesAsync();
            return role;
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            var role= await _imsContext.Roles.Where(r => r.Name == name).FirstOrDefaultAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(int id, Role role)
        {
            var checkRole = await _imsContext.Roles.FindAsync(id);
            _imsContext.Update(checkRole);
            await _imsContext.SaveChangesAsync();
            return checkRole;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _imsContext.Roles.FindAsync(id);
        }

        public async Task<Role> DeleteRoleAsyn(int id)
        {
            var role= await _imsContext.Roles.FindAsync(id);
             _imsContext.Remove(role);
             await _imsContext.SaveChangesAsync();
             return role;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _imsContext.Roles.ToListAsync();
        }
    }
}