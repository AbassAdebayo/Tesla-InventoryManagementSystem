using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class SalesService:ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IReturnGoodsRepository _returnGoodsRepository;


        public SalesService(ISalesRepository salesRepository, IItemRepository itemRepository, 
            IStockRepository stockRepository, IReturnGoodsRepository returnGoodsRepository)
        {
            _salesRepository = salesRepository;
            _itemRepository = itemRepository;
            _stockRepository = stockRepository;
            _returnGoodsRepository = returnGoodsRepository;
        }
      

        public async Task<BaseResponse<SalesDto>> FindSalesById(int id)
        {
            var sales = await _salesRepository.FindSalesById(id);
            if (sales==null)
            {
                return new BaseResponse<SalesDto>
                {
                    Message = "Sales requested cannot e found"
                };
            }

            return new BaseResponse<SalesDto>
            {
                Message = "Sales retrieved",
                Status = true,
                Data = new SalesDto
                {

                    
                    Description = sales.Description,
                    CustomerId = sales.CustomerId,
                    TotalPrice = sales.TotalPrice,
                    Id = sales.Id,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            };
        }

        public async Task<BaseResponse<SalesDto>> UpdateSales(int id, Sales sales)
        {
            var checkSales = await _salesRepository.FindSalesById(id);

            if (checkSales==null)
            {
                return new BaseResponse<SalesDto>
                {
                    Message = "Invalid request or null reference!",
                    Status = false
                };
            }

            await _salesRepository.UpdateSales(id, sales);
            return new BaseResponse<SalesDto>
            {
                Message = "Sales updated successfully",
                Status = true
            };
        }

        public async Task<bool> DeleteSales(int id)
        {
           await _salesRepository.DeleteSales(id);
           return true;
        }

        public async Task<BaseResponse<bool>> ExistsById(int id)
        {
            await _salesRepository.ExistsById(id);
            return new BaseResponse<bool>
            {
                Message = "Request successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IEnumerable<SalesDto>>> GetAllSales()
        {
            await _salesRepository.GetAllSales();
            return new BaseResponse<IEnumerable<SalesDto>>
            {
                Message = "Sales retrieved",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<Sales>>> SalesItem(CreateSalesRequestModel model)
        {
            var checkStockItem = await _stockRepository.GetStockItemsByItemId(model.StockItemId);
            var cart = model.SalesItems.Where(q=>q.Quantity > 0).ToDictionary(s => s.ItemId, s=>s.Quantity);
            var cartItem = await _stockRepository.GetAllStockItems(cart.Keys);
            var sales = new Sales
            {
                CustomerId = model.CustomerId,
                SalesManagerId = model.SalesManagerId,
                DateCreated = DateTime.UtcNow,
                CustomerEmailAddress = model.CustomerEmailAddress,

            };

            foreach (var stockItem in cartItem)
            {
                var quantity = cart[stockItem.Id];
                var pricePerUnit = stockItem.PricePerUnit;
                var salesItem = new SalesItem
                {
                    ItemId = stockItem.ItemId,
                    Item = stockItem.Item,
                    Quantity = quantity,
                    Sales = sales,
                    PricePerUnit = pricePerUnit,
                    DateCreated = DateTime.UtcNow

                };
                
                sales.TotalPrice += pricePerUnit * quantity;
                sales.SalesItems.Add(salesItem);
                checkStockItem.Quantity = checkStockItem.Quantity - salesItem.Quantity;
            }


            await _stockRepository.UpdateStockItem(model.StockItemId, checkStockItem);
            await _salesRepository.CreateSales(sales);
            
            return new BaseResponse<IList<Sales>>
            {
                Message = "Sales created successfully",
                Status = true,
                
            };
        }

        public async Task<BaseResponse<IList<SalesDto>>> GetSalesItemByDate(DateTime date)
        {
            var checkSalesDate= await _salesRepository.GetSalesItemByDate(date);
            if (checkSalesDate==null)
            {
                return new BaseResponse<IList<SalesDto>>
                {
                    Message = "Sales date requested cannot be found",
                    Status = false
                };
            }

            return new BaseResponse<IList<SalesDto>>
            {
                Message = "Data fetched successfully",
                Status = true,
                Data = checkSalesDate.Select(sales => new SalesDto
                {
                    
                    Description = sales.Description,
                    Id = sales.Id,
                    CustomerId = sales.CustomerId,
                    SalesManagerId = sales.SalesManagerId,
                    DateCreated = DateTime.UtcNow,
                    TotalPrice = sales.TotalPrice

                }).ToList()
            };

        }

        public async Task<BaseResponse<decimal>> GetGrandTotalOfAllSales()
        {
           var grandTotalSales= await _salesRepository.GetGrandTotalOfAllSales();

           return new BaseResponse<decimal>
           {
               Message = "Grand total sales fetched!",
               Status = true,
               Data = grandTotalSales
           };
        }

        public async Task<BaseResponse<ReturnGoodsDto>> ReturnGoods(int salesItemId, ReturnGoodsRequestModel model)
        {
            var checkSalesItem = await _salesRepository.FindSalesItemById(salesItemId);
            var sales = await _salesRepository.FindSalesById(model.SalesId);
            if (checkSalesItem==null)
            {
                return new BaseResponse<ReturnGoodsDto>
                {
                    Message = "SalesItem not found!",
                    Status = false
                };
            }
            
            if (DateTime.UtcNow > checkSalesItem.DateCreated.AddDays(7))
            {
                return new BaseResponse<ReturnGoodsDto>
                {
                    Message = "The time interval for returning goods has elapsed",
                    Status = false
                };
            }
            else if (DateTime.UtcNow <= checkSalesItem.DateCreated.AddDays(7))
            {
                var returnGoods = new ReturnGoods
                {
                    CustomerId = model.CustomerId,
                    Description = model.Description,
                    QuantityReturned = model.QuantityReturned,
                    SalesItemId = model.SalesItemId,
                    SalesManagerId = model.SalesManagerId,
                    ReturnType = model.ReturnType,
                    DateCreated = DateTime.UtcNow

                };
                checkSalesItem.Quantity = checkSalesItem.Quantity - returnGoods.QuantityReturned;
                sales.TotalPrice = checkSalesItem.Quantity * checkSalesItem.PricePerUnit;
           
                await _salesRepository.UpdateSalesItem(salesItemId, checkSalesItem);
                await _salesRepository.UpdateSales(sales.Id, sales);
                await _returnGoodsRepository.ReturnGoods(returnGoods);


            }
            return new BaseResponse<ReturnGoodsDto>
            {
                Message = "Goods successfully returned",
                Status = true
            };
        }
    }
}