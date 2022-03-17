using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Task<Stock> CreateStock(Stock stock);

        Task<Stock> UpdateStock(int id, Stock stock);

        Task<StockItem> CreateStockItem(StockItem stockItem);
        
        Task<StockItem> DeleteStockItem(int stockItemId);

        Task<Stock> DeleteStock(int id);

        Task<Stock> ExistsByName(string stockName);

        Task<Stock> GetStockById(int id);

        Task<StockItem> GetStockItemsByItemId(int itemId);
        
        Task<StockItem> GetStockItemById(int id);

        Task<IEnumerable<StockDto>> GetAllStocks();

        Task<IEnumerable<StockItem>> GetAllStockItems();

        Task<IList<StockItem>> GetAllStockItems(IEnumerable<int> stockItemIds);
        

        Task<decimal> CalculateGrandTotalPriceOfAllStockItem();
        

        Task<StockItem> UpdateStockItem(int id, StockItem stockItem);

    }
}