using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class ShopManagerService: IShopManagerService
    {
        private readonly IShopManagerRepository _managerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public ShopManagerService(IShopManagerRepository managerRepository, 
            IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _managerRepository = managerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        
        public async Task<BaseResponse<ShopManagerDto>> RegisterShopManager(RegisterShopManagerRequestModel model)
        {
            try
            {
                var role = await _roleRepository.GetRoleByNameAsync("ShopManager");
                var user = await _userRepository.GetUserByEmail(model.Email);

                if (user!=null)
                {
                    return new BaseResponse<ShopManagerDto>
                    {
                        Message = "Username already exist!",
                        Status = false
                    };
                }
            
                user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                };

                var userRole = new UserRole
                {
                    Role = role,
                    RoleId = role.Id,
                    User = user,
                    UserId = user.Id
                };
                user.UserRoles.Add(userRole);

           
                var newUser = await _userRepository.AddUserAsync(user);
                
                var shopManager = new ShopManager
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserId = newUser.Id,
                    User = user,
                    DateCreated = DateTime.UtcNow
                };
                await _managerRepository.AddShopManagerAsync(shopManager);
                return new BaseResponse<ShopManagerDto>
                {
                    Message = "Successfully created",
                    Status = true

                };

            }
            catch
            {
                throw new Exception();
            }
           
        }

        public async Task<BaseResponse<ShopManagerDto>> UpdateShopManager(int id, UpdateShopManagerRequestModel model)
        {
            try
            {
                var checkShopManager = await _managerRepository.GetShopManagerByIdAsync(id);

                if (checkShopManager==null)
                {
                    return new BaseResponse<ShopManagerDto>
                    {
                        Message = $"The Shop Manager with id {id} does not exist",
                        Status = false
                    };
                }

                checkShopManager.Address = model.Address;
                checkShopManager.Email = model.Email;
                checkShopManager.FirstName = model.FirstName;
                checkShopManager.LastName = model.LastName;
                checkShopManager.PhoneNumber = model.PhoneNumber;
                await _managerRepository.UpdateShopManagerAsync(id, checkShopManager);
                return new BaseResponse<ShopManagerDto>
                {
                    Message = "User Successfully updated",
                    Status = true,

                };
            }
            catch
            {
                throw new Exception();
            }
            
            
        }

        public async Task<BaseResponse<bool>> DeleteShopManager(int id)
        {
            try
            {
                var checkShopManager = await _managerRepository.GetShopManagerByIdAsync(id);
                if (checkShopManager==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The Shop Manager with id {id} does not exist",
                        Status = false
                    };
                }

                await _managerRepository.DeleteShopManagerAsync(checkShopManager);
                return new BaseResponse<bool>
                {
                    Message = "User Successfully deleted",
                    Status = true,
                };
            }
            catch 
            {
                
                throw new Exception();
            }
            
        }

        public async Task<BaseResponse<ShopManagerDto>> GetShopManagerById(int id)
        {
            try
            {
                var checkShopManager = await _managerRepository.GetShopManagerByIdAsync(id);
                if (checkShopManager==null)
                {
                    return new BaseResponse<ShopManagerDto>
                    {
                        Message = $"The user with id {id} does not exist",
                        Status = false,
                        
                    };
                }

                return new BaseResponse<ShopManagerDto>
                {
                
                    Message = $"The user with id {id} retrieved",
                    Status = true,
                    Data = new ShopManagerDto
                    {
                        Id = checkShopManager.Id,
                        Address = checkShopManager.Address,
                        Email = checkShopManager.Email,
                        FirstName = checkShopManager.FirstName,
                        LastName = checkShopManager.LastName,
                        PhoneNumber = checkShopManager.PhoneNumber,
                        
                        

                    }
                };
            }
            catch
            {
                throw new Exception();
            }
            
        }

        public async Task<BaseResponse<IList<ShopManagerDto>>> GetAllShopManagers()
        {
            try
            {
                var shopManagers = await _managerRepository.GetAllShopManagers();
                return new BaseResponse<IList<ShopManagerDto>>
                {
                    Message = "Users retrieved",
                    Status = true,
                    Data = shopManagers.Select(s=>new ShopManagerDto
                    {
                        Address = s.Address,
                        Email = s.Email,
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        PhoneNumber = s.PhoneNumber,
                        

                    }).ToList()
                
                };
            }
            catch
            {
                throw new Exception();
            }
            
        }
    }
}