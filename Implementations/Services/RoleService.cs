using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Role> AddRoleAsync(CreateRoleRequestModel model)
        {
            var newRole = new Role
            {
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsDeleted = false,
                Description = model.Description,
                Name = model.Name
            };
           await _roleRepository.AddRoleAsync(newRole);
           return newRole;
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            var roleCheck = await _roleRepository.GetRoleByNameAsync(name);
            return new Role
            {
                Description = roleCheck.Description,
                Name = roleCheck.Name,
                DateCreated = DateTime.UtcNow,
                IsDeleted = false,
                Id = roleCheck.Id

            };
        }

        public async Task<Role> UpdateRoleAsync(int id, UpdateRoleRequestModel model)
        {
            var checkRole = await _roleRepository.GetRoleById(id);
            checkRole.Description = model.Description;
            checkRole.Name = model.Name;
            return checkRole;
        }

        public async Task<Role> DeleteRoleAsync(int id)
        {
           return await _roleRepository.DeleteRoleAsyn(id);
           
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();

            return new List<Role>(roles.Select(r => new Role
            {
                Id = r.Id,
                Description = r.Description,
                Name = r.Name,
                DateCreated = DateTime.UtcNow
            }).ToList());
        }
    }
}