using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;


namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IItemRepository _itemRepository;

        public StockService(IStockRepository stockRepository, IItemRepository itemRepository)
        {
            _stockRepository = stockRepository;
            _itemRepository = itemRepository;
        }

        public async Task<BaseResponse<StockDto>> CreateStock(CreateStockRequestModel model)
        {
            var stock = await _stockRepository.ExistsByName(model.StockName);

            if (stock != null)
            {
                return new BaseResponse<StockDto>
                {
                    Status = false,
                    Message = "The Stock with this already exist!"
                };
            }

            var newStock = new Stock
            {
                Description = model.Description,
                SupplierId = model.SupplierId,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                StockName = model.StockName,
            };


            await _stockRepository.CreateStock(newStock);

            return new BaseResponse<StockDto>
            {
                Message = "Stock Successfully created",
                Status = true
            };
        }

        public async Task<BaseResponse<StockDto>> UpdateStock(int id, UpdateStockRequestModel model)
        {
            var stock = await _stockRepository.GetStockById(id);
            if (stock != null)
            {
                stock.Description = model.Description;
                stock.StockName = model.StockName;
                stock.SupplierId = model.SupplierId;
                stock.DateModified=DateTime.UtcNow;
            }

            await _stockRepository.UpdateStock(id, stock);
            return new BaseResponse<StockDto>
            {
                Message = "Stock successfully updated",
                Status = true
            };
        }

        public async Task<BaseResponse<StockDto>> DeleteStock(int id)
        {
            await _stockRepository.DeleteStock(id);

            return new BaseResponse<StockDto>
            {
                Message = "Stock successfully deleted",
                Status = true
            };
        }

        public async Task<BaseResponse<StockItem>> DeleteStockItem(int stockItemId)
        {
            await _stockRepository.DeleteStock(stockItemId);

            return new BaseResponse<StockItem>
            {
                Message = "Stock successfully deleted",
                Status = true
            };
        }

        public async Task<Stock> ExistsByName(string stockName)
        {
            return await _stockRepository.ExistsByName(stockName);
        }

        public async Task<StockDto> GetStockById(int id)
        {
            var stockCheck = await _stockRepository.GetStockById(id);
            if (stockCheck != null)
            {
                return new StockDto
                {

                    Id = stockCheck.Id,
                    StockName = stockCheck.StockName,
                    Description = stockCheck.Description,
                    SupplierId = stockCheck.SupplierId,
                    DateCreated = stockCheck.DateCreated
                    
                };
            }

            throw new Exception("Stock doesn't exist!");
        }

        public async Task<StockItem> GetStockItemById(int id)
        {
            return await _stockRepository.GetStockItemById(id);
           
        }

        public async Task<IEnumerable<StockDto>> GetAllStocks()
        {
            try
            {
                var stocks = await _stockRepository.GetAllStocks();
                return stocks.Select(s => new StockDto
                {
                    Id = s.Id,
                    StockName = s.StockName,
                    Description = s.Description,
                     SupplierId = s.SupplierId,
                     DateCreated = s.DateCreated
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<StockDto>> AddItemToStock(int id, AddItemToStockRequestModel model)
        {
            
            var stock = await _stockRepository.GetStockById(model.StockId);
            
            var itemInStock = await _stockRepository.GetStockItemsByItemId(model.ItemId);

            try
            {

                if (itemInStock == null)
                {
                    var newStockItem = new StockItem
                    {
                        StockName = model.StockName,
                        ItemId = model.ItemId,
                        Stock = stock,
                        StockId = model.StockId,
                        Quantity = model.Quantity,
                        PricePerUnit = model.PricePerUnit,
                        TotalPrice = model.PricePerUnit * model.Quantity,
                        DateCreated = DateTime.UtcNow
                        
                    };

                    await _stockRepository.CreateStockItem(newStockItem);
                }
                else
                {
                    itemInStock.Quantity += model.Quantity;
                    itemInStock.PricePerUnit = model.PricePerUnit;
                    itemInStock.TotalPrice += model.PricePerUnit * model.Quantity;
                    await _stockRepository.UpdateStockItem(id, itemInStock);
                }


                return new BaseResponse<StockDto>
                {
                    Message = "Item successfully added to stock",
                    Status = true,
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<StockDto>> UpdateItemInStock(int stockId, UpdateStockItemRequestModel model)
        {
            
            var itemInStock = await _stockRepository.GetStockItemById(model.StockItemId);
            if (itemInStock==null)
            {
                return new BaseResponse<StockDto>
                {
                    Message = "The item fetched does not exist in stock",
                    Status = false
                };
            }

            itemInStock.Quantity = model.Quantity;
            itemInStock.PricePerUnit = model.PricePerUnit;
            itemInStock.TotalPrice = model.PricePerUnit * model.Quantity;
            itemInStock.ItemId = model.ItemId;
            itemInStock.StockId = model.StockId;
            itemInStock.DateModified=DateTime.UtcNow;
            await _stockRepository.UpdateStockItem(stockId, itemInStock);
            return new BaseResponse<StockDto>
            {
                Message = "Data updated",
                Status = true,
                
                
            };
        }


        public async Task<decimal> CalculateGrandTotalPriceOfAllStockItem()
        {
           return await _stockRepository.CalculateGrandTotalPriceOfAllStockItem();
           
        }

        public async Task<IEnumerable<StockItem>> GetAllStockItems()
        {
            var stockItems = await _stockRepository.GetAllStockItems();

             return stockItems.Select(stockItem => new StockItem()
            {
                Id = stockItem.Id,
                StockName = stockItem.StockName,
                Item = stockItem.Item,
                Stock = stockItem.Stock,
                PricePerUnit = stockItem.PricePerUnit,
                Quantity = stockItem.Quantity,
                TotalPrice = stockItem.TotalPrice,
                DateCreated = stockItem.DateCreated

            }).ToList();
        }

        public async Task<decimal> GetExpenses()
        {
            return await _stockRepository.GetExpenses();
        }

        public async Task<IList<StockItem>> GetStockItemByDate(DateTime date)
        {
            return await _stockRepository.GetStockItemByDate(date);
        }
    }
}