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
    public class StockKeeperService: IStockKeeperService
    {
        private readonly IStockKeeperRepository _stockKeeperRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public StockKeeperService(IStockKeeperRepository stockKeeperRepository, 
            IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _stockKeeperRepository = stockKeeperRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        
        public async Task<BaseResponse<bool>> RegisterStockKeeper(RegisterStockKeeperRequestModel model)
        {


            try
            {
                var role = await _roleRepository.GetRoleByNameAsync("StockKeeper");
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
                var stockKeeper = new StockKeeper
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
                await _stockKeeperRepository.AddStockKeeperAsync(stockKeeper);
                return new BaseResponse<bool>
                {
                    Message = "Successfully created",
                    Status = true

                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<bool>> UpdateStockKeeper(int id, UpdateStockKeeperRequestModel model)
        {
            var checkStockKeeper = await _stockKeeperRepository.GetStockKeeperByIdAsync(id);

            if (checkStockKeeper==null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"The Stock Keeper with id {id} does not exist",
                    Status = false
                };
            }

            checkStockKeeper.Address = model.Address;
            checkStockKeeper.Email = model.Email;
            checkStockKeeper.FirstName = model.FirstName;
            checkStockKeeper.LastName = model.LastName;
            checkStockKeeper.User.UserName = model.UserName;
            checkStockKeeper.PhoneNumber = model.PhoneNumber;
            await _stockKeeperRepository.UpdateStockKeeperAsync(id, checkStockKeeper);
            return new BaseResponse<bool>
            {
                Message = "User Successfully updated",
                Status = true,

            };
        }

        public async Task<BaseResponse<bool>> DeleteStockKeeper(int id)
        {
            var checkStockKeeper = await _stockKeeperRepository.GetStockKeeperByIdAsync(id);
            if (checkStockKeeper==null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"The Stock Keeper with id {id} does not exist",
                    Status = false
                };
            }

            await _stockKeeperRepository.DeleteStockKeeperAsync(checkStockKeeper);
            return new BaseResponse<bool>
            {
                Message = "User Successfully deleted",
                Status = true,
            };
        }

        public async Task<StockKeeperDto> GetStockKeeperByEmail(string email)
        {
            var checkStockKeeper = await _userRepository.GetUserByEmail(email);
            if (checkStockKeeper==null)
            {
                throw new Exception("Information requested doesn't exist!");
            }

            return new StockKeeperDto
            {
                Id = checkStockKeeper.Id,
                Email = checkStockKeeper.Email,
                DateCreated = checkStockKeeper.DateCreated,
                //UserName = checkStockKeeper.UserName
                
            };
        }

        public async Task<StockKeeperDto> GetStockKeeperById(int id)
        {
            var checkStockKeeper = await _stockKeeperRepository.GetStockKeeperByIdAsync(id);
            if (checkStockKeeper!=null)
            {
                return new StockKeeperDto
                {
                    Id = checkStockKeeper.Id,
                    Address = checkStockKeeper.Address,
                    Email = checkStockKeeper.Email,
                    FirstName = checkStockKeeper.FirstName,
                    LastName = checkStockKeeper.LastName,
                    PhoneNumber = checkStockKeeper.PhoneNumber,
                    DateCreated = checkStockKeeper.DateCreated,
                    //UserName = checkStockKeeper.User.UserName
                
                };
                
            }

            throw new Exception("Information requested doesn't exist!");
        }

        public async Task<IList<StockKeeperDto>> GetAllStockKeepers()
        {
            var stockKeeper = await _stockKeeperRepository.GetAllStockKeepers();


            return stockKeeper.Select(s => new StockKeeperDto
            {
                Address = s.Address,
                Email = s.Email,
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNumber = s.PhoneNumber,
                DateCreated = s.DateCreated
            }).ToList();


        }
    }
}