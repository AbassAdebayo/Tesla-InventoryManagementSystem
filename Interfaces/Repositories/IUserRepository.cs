using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User> AddUserAsync(User user);

        public Task<User> UpdateUserAsync(int id, User user);

        Task<User> DeleteUserAsync(User user);
        
        Task<User> GetUserByUserNameAsync(string userName);

        Task<User> GetUserById(int id);

        Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password);
        
        Task<IList<User>> GetAllUsersAsync();

        Task<User> GetUserByEmail(string email);


    }
}