using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IStockKeeperRepository
    {
        public Task<StockKeeper> AddStockKeeperAsync(StockKeeper stockKeeper);
        
        public Task<StockKeeper> UpdateStockKeeperAsync(int id, StockKeeper stockKeeper);
        
        public Task<StockKeeper> DeleteStockKeeperAsync(StockKeeper stockKeeper);
        
        public Task<StockKeeper> GetStockKeeperByUsernameAsync(string userName);
        
        public Task<StockKeeper> GetStockKeeperByIdAsync(int id);
        
        public Task<IEnumerable<StockKeeper>> GetAllStockKeepers();
    }
}