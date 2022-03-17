﻿using System.Collections.Generic;
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
        
        Task<BaseResponse<StockItem>> DeleteStockItem(int stockItemId);

        Task<Stock> ExistsByName(string stockName);

        Task<BaseResponse<StockDto>> GetStockById(int Id);
        
        
        Task<BaseResponse<StockItem>> GetStockItemById(int Id);

        Task<BaseResponse<IList<StockDto>>> GetAllStocks();

        Task<BaseResponse<StockDto>> AddItemToStock(int id, AddItemToStockRequestModel model);

        Task<BaseResponse<StockDto>> UpdateItemInStock(int itemId, UpdateStockItemRequestModel model);

        Task<BaseResponse<decimal>> CalculateGrandTotalPriceOfAllStockItem();
        
        
    }
}