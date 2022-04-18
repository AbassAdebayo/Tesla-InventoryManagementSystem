using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;


namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IStockService
    {
        Task<BaseResponse<StockDto>> CreateStock(CreateStockRequestModel model);

        Task<BaseResponse<StockDto>> UpdateStock(int id, UpdateStockRequestModel model);

        Task<BaseResponse<StockDto>> DeleteStock(int id);
        
        Task<Stock> ExistsByName(string stockName);

        Task<StockDto> GetStockById(int id);
        
        
        Task<StockItem> GetStockItemById(int id);

        Task<IEnumerable<StockDto>> GetAllStocks();

        Task<BaseResponse<StockDto>> AddItemToStock(int id, AddItemToStockRequestModel model);

        Task<BaseResponse<StockDto>> UpdateItemInStock(int itemId, UpdateStockItemRequestModel model);
        
        Task<IEnumerable<StockItem>> GetAllStockItems();

        Task<decimal> GetExpenses();

        Task<IList<StockItem>> GetStockItemByDate(DateTime date);


    }
}