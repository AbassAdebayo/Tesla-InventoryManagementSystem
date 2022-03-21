using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using InventoryManagemenSystem_Ims.Models;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class SalesManagerService:ISalesManagerService
    {
        private readonly ISalesManagerRepository _salesManagerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public SalesManagerService(ISalesManagerRepository salesManagerRepository, 
            IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _salesManagerRepository = salesManagerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<bool>> RegisterSalesManager(RegisterSalesManagerRequestModel model)
        {
            try
            {
                var role = await _roleRepository.GetRoleByNameAsync("SalesManager");
                var user = await _userRepository.GetUserByEmail(model.Email);

                if (user!=null)
                {
                    return new BaseResponse<bool>
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
                var salesManager = new SalesManager
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
                await _salesManagerRepository.AddSalesManagerAsync(salesManager);
            
                return new BaseResponse<bool>
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

        public async Task<BaseResponse<bool>> UpdateSalesManager(int id, UpdateSalesManagerRequestModel model)
        {
            try
            {
                var checkSalesManager = await _salesManagerRepository.GetSalesManagerByIdAsync(id);

                if (checkSalesManager==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The Shop Manager with id {id} does not exist",
                        Status = false
                    };
                }

                checkSalesManager.Address = model.Address;
                checkSalesManager.Email = model.Email;
                checkSalesManager.FirstName = model.FirstName;
                checkSalesManager.LastName = model.LastName;
                checkSalesManager.User.UserName = model.UserName;
                checkSalesManager.PhoneNumber = model.PhoneNumber;
                await _salesManagerRepository.UpdateSalesManagerAsync(id, checkSalesManager);
                return new BaseResponse<bool>
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

        public async Task<BaseResponse<bool>> DeleteSalesManager(int id)
        {
            try
            {
                var checkSalesManager = await _salesManagerRepository.GetSalesManagerByIdAsync(id);
                if (checkSalesManager==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The Sales Manager with id {id} does not exist",
                        Status = false
                    };
                }

                await _salesManagerRepository.DeleteSalesManagerAsync(checkSalesManager);
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

        public async Task<SalesManagerDto> GetSalesManagerById(int id)
        {
            try
            {
                var checkSalesManager = await _salesManagerRepository.GetSalesManagerByIdAsync(id);
                if (checkSalesManager !=null)
                {
                    return new SalesManagerDto
                    {
                        Id = checkSalesManager.Id,
                        Address = checkSalesManager.Address,
                        Email = checkSalesManager.Email,
                        FirstName = checkSalesManager.FirstName,
                        LastName = checkSalesManager.LastName,
                        PhoneNumber = checkSalesManager.PhoneNumber,
                        DateCreated = checkSalesManager.DateCreated
                        
                        //UserName = checkSalesManager.User.UserName
                    
                    };
                }
                
                throw new Exception("Information doesn't exist!");
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<IEnumerable<SalesManagerDto>> GetAllSalesManagers()
        {
            try
            {
                var salesManagers = await _salesManagerRepository.GetAllSalesManagers();
                return new List<SalesManagerDto>(salesManagers.Select(s => new SalesManagerDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Address = s.Address,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    //UserName = s.User.UserName

                }).ToList());

            }
            catch
            {
                throw new Exception();
            }
        }
    }
}