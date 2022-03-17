using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ImsContext _imsContext;

        public UserRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        
        public async Task<User> AddUserAsync(User user)
        {
            await _imsContext.Users.AddAsync(user);
            await _imsContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        { 
            _imsContext.Update(user);
            await _imsContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUserAsync(User user)
        {
             _imsContext.Users.Remove(user);
             await _imsContext.SaveChangesAsync();
             return user;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _imsContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _imsContext.Users.FindAsync(id);
            
        }

        public async Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            var user = await _imsContext.Users.Include(x=>x.UserRoles).Include(u=>u.UserRoles).ThenInclude(x=>x.Role)
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password);

            return user;
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _imsContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _imsContext.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}