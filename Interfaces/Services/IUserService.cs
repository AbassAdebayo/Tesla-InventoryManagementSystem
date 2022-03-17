using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> Login(LoginDto model);
        
        Task<BaseResponse<UserDto>> GetUserByUserName(string userName);
        
        Task<BaseResponse<UserDto>> GetUserByEmail(string email);

        Task<BaseResponse<UserDto>> GetUserById(int id);

        Task<BaseResponse<UserDto>> GetUserByUserNameAndPassword(string userName, string password);
        
        Task<BaseResponse<IList<UserDto>>> GetAllUsers();

        Task<BaseResponse<bool>> DeleteUser(string userName);

        Task<BaseResponse<UserDto>> UpdateUser(int id, UpdateUserRequestModel model);
    }
}