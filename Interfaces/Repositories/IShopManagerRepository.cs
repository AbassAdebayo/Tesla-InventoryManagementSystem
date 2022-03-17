using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IShopManagerRepository
    {
        public Task<ShopManager> AddShopManagerAsync(ShopManager shopManager);
        
        public Task<ShopManager> UpdateShopManagerAsync(int id, ShopManager shopManager);
        
        public Task<ShopManager> DeleteShopManagerAsync(ShopManager shopManager);
        
        public Task<ShopManager> GetShopManagerByUsernameAsync(string userName);
        
        public Task<ShopManager> GetShopManagerByIdAsync(int id);
        
        public Task<IEnumerable<ShopManager>> GetAllShopManagers();
    }
}