using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class StockKeeperRepository:IStockKeeperRepository
    {
        private readonly ImsContext _imsContext;

        public StockKeeperRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<StockKeeper> AddStockKeeperAsync(StockKeeper stockKeeper)
        {
            await _imsContext.StockKeepers.AddAsync(stockKeeper);
            await _imsContext.SaveChangesAsync();
            return stockKeeper;
        }

        public async Task<StockKeeper> UpdateStockKeeperAsync(int id, StockKeeper stockKeeper)
        {
            _imsContext.Update(stockKeeper);
            await _imsContext.SaveChangesAsync();
            return stockKeeper;
        }

        public async Task<StockKeeper> DeleteStockKeeperAsync(StockKeeper stockKeeper)
        {
            _imsContext.StockKeepers.Remove(stockKeeper);
            await _imsContext.SaveChangesAsync();
            return stockKeeper;
        }

        public async Task<StockKeeper> GetStockKeeperByUsernameAsync(string userName)
        {
           return await _imsContext.StockKeepers.FirstOrDefaultAsync(u => u.User.UserName == userName);
        }

        public async Task<StockKeeper> GetStockKeeperByEmailAsync(string email)
        {
            return await _imsContext.StockKeepers.FirstOrDefaultAsync(u => u.User.Email == email);
        }

        public async Task<StockKeeper> GetStockKeeperByIdAsync(int id)
        {
            return await _imsContext.StockKeepers.FindAsync(id);
        }

        public async Task<IEnumerable<StockKeeper>> GetAllStockKeepers()
        {
            return await _imsContext.StockKeepers.ToListAsync();
        }
    }
}