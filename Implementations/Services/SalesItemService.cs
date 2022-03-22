using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class SalesItemService:ISalesItemService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IStockService _stockService;
        private readonly IStockRepository _stockRepository;


        public SalesItemService(ISalesRepository salesRepository, IStockService stockService, 
            IStockRepository stockRepository)
        {
            _salesRepository = salesRepository;
            _stockService = stockService;
            _stockRepository = stockRepository;
        }
        public async Task<SalesItem> CreateSalesItem(SalesItem salesItem)
        {
            await _salesRepository.CreateSalesItem(salesItem);
            return salesItem;
        }

        public async Task<SalesItem> UpdateSalesItem(UpdateSalesItemRequestModel model)
        {
            var checkSalesItem = await _salesRepository.FindSalesItemById(model.SalesItemId);
            var sales = await _salesRepository.FindSalesById(model.SalesId);

            if (checkSalesItem==null)
            {
                return null;
            }

            checkSalesItem.ItemId = model.ItemId;
            checkSalesItem.Quantity = model.Quantity;
            checkSalesItem.SalesId = model.SalesId;
            checkSalesItem.PricePerUnit = model.PricePerUnit;
            sales.TotalPrice = checkSalesItem.PricePerUnit * checkSalesItem.Quantity;
            await _salesRepository.UpdateSalesItem(model.SalesItemId, checkSalesItem);
            await _salesRepository.UpdateSales(model.SalesId, sales);
            return checkSalesItem;
        }

        public async Task<SalesItem> FindSalesItemById(int id)
        {
            return await _salesRepository.FindSalesItemById(id);
        }

        public async Task<BaseResponse<bool>> DeleteSalesItem(int id, int stockItemId)
        {
            var stockItem = await _stockService.GetStockItemById(stockItemId);
            var salesItem = await _salesRepository.GetSalesItemById(id);
            
            if (salesItem==null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"The SalesItem with id {id} does not exist!",
                    Status = false
                };
            }
            
            
            var newStockItem = new UpdateStockItemRequestModel
            {
              StockItemId = stockItem.Id,
                Quantity = salesItem.Quantity + stockItem.Quantity,
                ItemId = stockItem.ItemId,
                PricePerUnit = stockItem.PricePerUnit,
                StockId = stockItem.StockId,
                
            };
            
            await _stockService.UpdateItemInStock(stockItemId, newStockItem);
            await _salesRepository.DeleteSalesItem(salesItem.Id);
            
            return new BaseResponse<bool>
            {
                Message = "SalesItem deleted successfully",
                Status = true
            };
        }

        public async Task<BaseResponse<bool>> DeleteSalesItems()
        {
            var sales = await _salesRepository.GetAllSalesItems();
            var stockItems = await _stockRepository.GetAllStockItems();
            //var checkStockItem = await _stockService.GetStockItemById(stockItemId);
            //var salesItem = await _salesRepository.GetSalesItemById(salesItemId);

           
            
                foreach (var stockItem in stockItems)
                {
                    foreach (var sale in sales)
                    {
                        var newStockItem = new UpdateStockItemRequestModel
                        {
                            StockItemId = stockItem.Id,
                            StockId = stockItem.StockId,
                            ItemId = stockItem.ItemId,
                            PricePerUnit = stockItem.PricePerUnit,
                            Quantity = stockItem.Quantity + sale.Quantity
                            
                            
                        };
                        await _stockService.UpdateItemInStock(stockItem.Id, newStockItem);
                        await _salesRepository.DeleteSalesItem(sale.Id);
                    }
                   
                    
                }
            
            return new BaseResponse<bool>
            {
                Message = "SalesItem deleted successfully",
                Status = true
            };
        }

        public async Task<IList<SalesItem>> GetAllSalesItems()
        {
            return await _salesRepository.GetAllSalesItems();
        }

        // public Task<IEnumerable<SalesItem>> GetAllSalesItemBySalesManagerId(int salesManagerId)
        // {
        //     throw new NotImplementedException();
        // }
    }
}