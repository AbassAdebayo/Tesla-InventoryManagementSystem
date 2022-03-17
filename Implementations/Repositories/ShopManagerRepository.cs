using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class ShopManagerRepository : IShopManagerRepository
    {
        private readonly ImsContext _imsContext;

        public ShopManagerRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }

        public async Task<ShopManager> AddShopManagerAsync(ShopManager shopManager)
        {
            await _imsContext.ShopManagers.AddAsync(shopManager);
            await _imsContext.SaveChangesAsync();
            return shopManager;
        }

        public async Task<ShopManager> UpdateShopManagerAsync(int id, ShopManager shopManager)
        {
            _imsContext.Update(shopManager);
            await _imsContext.SaveChangesAsync();
            return shopManager;
        }

        public async Task<ShopManager> DeleteShopManagerAsync(ShopManager shopManager)
        {
            _imsContext.ShopManagers.Remove(shopManager);
            await _imsContext.SaveChangesAsync();
            return shopManager;
        }

        public async Task<ShopManager> GetShopManagerByUsernameAsync(string userName)
        {
            return await _imsContext.ShopManagers.FirstOrDefaultAsync(u => u.User.UserName == userName);
        }

        public async Task<ShopManager> GetShopManagerByIdAsync(int id)
        {
            return await _imsContext.ShopManagers.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IEnumerable<ShopManager>> GetAllShopManagers()
        {
            return await _imsContext.ShopManagers.Include(x=>x.User.UserName).Include(x=>
                x.Address).Include(x=>x.FirstName).Include(x=>x.LastName).Include(x=>x.PhoneNumber).Include(x=>x.User.UserRoles).ToListAsync();
        }
    }
}