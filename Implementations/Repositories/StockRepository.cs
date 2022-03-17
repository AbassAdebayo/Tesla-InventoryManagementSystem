using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    
    public class StockRepository: IStockRepository
    {
        private readonly ImsContext _imsContext;

        public StockRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        
        public async Task<Stock> CreateStock(Stock stock)
        {
            await _imsContext.Stocks.AddAsync(stock);
            await _imsContext.SaveChangesAsync();
            return stock;

        }

        public async Task<Stock> UpdateStock(int id, Stock stock)
        {
           var checkStock= await _imsContext.Stocks.FindAsync(id);
           _imsContext.Update(checkStock);
          await _imsContext.SaveChangesAsync();
           return checkStock;
           
        }

        public async Task<StockItem> CreateStockItem(StockItem stockItem)
        {
            await _imsContext.StockItems.AddAsync(stockItem);
            await _imsContext.SaveChangesAsync();
            return stockItem;
        }

        public async Task<StockItem> DeleteStockItem(int stockItemId)
        {
            var checkStockItem= await _imsContext.StockItems.FindAsync(stockItemId);
            _imsContext.StockItems.Remove(checkStockItem);
            await _imsContext.SaveChangesAsync();
            return checkStockItem;
        }

        public async Task<Stock> DeleteStock(int id)
        {
          var checkStock= await _imsContext.Stocks.FindAsync(id);
          _imsContext.Stocks.Remove(checkStock);
          await _imsContext.SaveChangesAsync();
          return checkStock;
        }

        public async Task<Stock> ExistsByName(string stockName)
        {
            return await _imsContext.Stocks.FirstOrDefaultAsync(x => x.StockName == stockName);

        }

        public async Task<Stock> GetStockById(int id)
        {
            return await _imsContext.Stocks.FindAsync(id);
        }

        public async Task<StockItem> GetStockItemById(int id)
        {
            return await _imsContext.StockItems.FindAsync(id);
        }

        public async Task<IEnumerable<StockDto>> GetAllStocks()
        {
            return await _imsContext.Stocks.Select(stock=>new StockDto
            {
                Description = stock.Description,
                Id = stock.Id,
                DateCreated = stock.DateCreated,
                DateModified = stock.DateModified,
                StockName = stock.StockName,
                SupplierId = stock.Supplier.Id,
                
            }).ToListAsync();
        }

        public async Task<IList<StockItem>> GetAllStockItems(IEnumerable<int> stockItemIds)
        {
            return await _imsContext.StockItems.Where(stockItem => stockItemIds.Contains(stockItem.Id)).ToListAsync();
        }

        public async Task<decimal> CalculateGrandTotalPriceOfAllStockItem()
        {
            return await _imsContext.StockItems.SumAsync(t => t.TotalPrice);
        }

       

        public async Task<IEnumerable<StockItem>> GetAllStockItems()
        {
            return await _imsContext.StockItems.ToListAsync();
        }
        
        public async Task<StockItem> GetStockItemsByItemId(int itemId)
        {
            return await _imsContext.StockItems.FirstOrDefaultAsync(s => s.ItemId == itemId);
        }

        public async Task<StockItem> UpdateStockItem(int id, StockItem stockItem)
        {
            _imsContext.StockItems.Update(stockItem);
            await _imsContext.SaveChangesAsync();
            return stockItem;
        }
    }
}