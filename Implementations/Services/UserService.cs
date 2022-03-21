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
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse<UserDto>> Login(LoginDto model)
        {
           
            var user = await _userRepository.GetUserByEmail(model.Email);

            if (user==null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Invalid username or password",
                    Status = false
                };
            }
            
            var userVerify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (userVerify==false)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Invalid username or password",
                    Status = false
                };
            }

            return new BaseResponse<UserDto>
            {
                Status = true, 
                Data = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(r=>new RoleDto
                    {
                        Id = r.Role.Id,
                        Name = r.Role.Name,
                        Description = r.Role.Description
                            
                    }).ToList()
                },
                Message = "Login successful"
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserByUserName(string userName)
        {
            var user = await _userRepository.GetUserByUserNameAsync(userName);
            if (user==null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User cannot be found",
                    Status = false
                };
            }

            return new BaseResponse<UserDto>
            {
                Message = "User retrieved",
                Status = true,
                Data = new UserDto
                {
                    Username = user.UserName
                }
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user==null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User cannot be found",
                    Status = false
                };
            }

            return new BaseResponse<UserDto>
            {
                Message = "User retrieved",
                Status = true,
                Data = new UserDto
                {
                    Username = user.UserName,
                    Email = user.Email
                }
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user==null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User does not exist",
                    Status = false,

                };
            }

            return new BaseResponse<UserDto>
            {
                Data = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(x=>new RoleDto
                    {
                        Id = x.Id,
                        Name = x.Role.Name,
                        Description = x.Role.Description
                    }).ToList()

                }
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserByUserNameAndPassword(string userName, string password)
        {
            var user = await _userRepository.GetUserByUserNameAndPasswordAsync(userName, password);
            if (user==null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User cannot be found",
                    Status = false
                };
            }

            return new BaseResponse<UserDto>
            {
                Message = "User retrieved",
                Status = true,
                Data = new UserDto
                {
                    Username = user.UserName
                }
            };
        }
        

        public async Task<BaseResponse<IList<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return new BaseResponse<IList<UserDto>>
            {
                Message = "Users fetched",
                Status = true,
                Data = users.Select(u => new UserDto
                {
                    Username = u.UserName,
                    Email = u.Email

                }).ToList()
            };
        }

        public async Task<BaseResponse<bool>> DeleteUser(string userName)
        {
            try
            {
                var user = await _userRepository.GetUserByUserNameAsync(userName);
                if (user==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The user with id {userName} does not exist",
                        Status = false
                    };
                }

                await _userRepository.DeleteUserAsync(user);
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

        public async Task<BaseResponse<UserDto>> UpdateUser(int id, UpdateUserRequestModel model)
        {
            var user = await _userRepository.GetUserById(id);

            if (user==null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User does not exist!",
                    Status = false,
                    
                };
            }

            user.UserName = model.Username;
            user.Email = model.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            await _userRepository.UpdateUserAsync(id, user);
            return new BaseResponse<UserDto>()
            {
                Message = "User updated successfully",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email
                }
            };
        }

        public bool GetUserByRole(string roleName)
        {
            _userRepository.GetUserByRole(roleName);
            return true;
        }
    }
}