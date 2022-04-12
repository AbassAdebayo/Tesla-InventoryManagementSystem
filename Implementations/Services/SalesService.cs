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
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class SalesService:ISalesService
    {
        private readonly ISalesRepository _salesRepository;
      
        private readonly IStockService _stockService;
       
        private readonly IReturnGoodsRepository _returnGoodsRepository;
       // private readonly IAllocateSalesItemToSalesManagerRepository _allocateSalesItemToSalesManager;


        public SalesService(ISalesRepository salesRepository,
            IStockService stockService, IReturnGoodsRepository returnGoodsRepository)
        {
            _salesRepository = salesRepository;
            _stockService = stockService;
            _returnGoodsRepository = returnGoodsRepository;
            //_allocateSalesItemToSalesManager = allocateSalesItemToSalesManager;
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
                    PricePerUnit = sales.PricePerUnit,
                    Quantity = sales.Quantity,
                    TotalPrice = sales.PricePerUnit * sales.Quantity,
                    Id = sales.Id,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            };
        }

        public async Task<Sales> UpdateSales(UpdateSalesRequestModel model)
        {
            var sales = await _salesRepository.FindSalesById(model.SalesId);
           // var checkSalesItem = await _salesRepository.FindSalesItemById(model.SalesItemId);
            

            if (sales==null)
            {
                return null;
            }

            sales.ItemId = model.ItemId;
            sales.Id = model.SalesId;
            sales.PricePerUnit = model.PricePerUnit;
            sales.TotalPrice = sales.PricePerUnit * sales.Quantity;
            await _salesRepository.UpdateSales(model.SalesId, sales);
            return sales;
        }

        // public async Task<bool> DeleteSales(int id, int stockItemId)
        // {
        //     var stockItem = await _stockService.GetStockItemById(stockItemId);
        //     var sales = await _salesRepository.FindSalesById(id);
        //
        //     if (sales == null)
        //     {
        //         throw new Exception("Sales not found!");
        //     }
        //
        //
        //     var newStockItem = new UpdateStockItemRequestModel
        //     {
        //         StockItemId = stockItemId,
        //         Quantity = sales.Quantity + stockItem.Quantity,
        //         ItemId = stockItem.ItemId,
        //         PricePerUnit = stockItem.PricePerUnit,
        //         StockId = stockItem.StockId,
        //
        //     };
        //
        //     await _stockService.UpdateItemInStock(stockItemId, newStockItem);
        //     await _salesRepository.DeleteSales(sales.Id);
        //     return true;
        // }
        

        public async Task<IEnumerable<Sales>> GetAllSales()
        {
            var sales = await _salesRepository.GetAllSales();

            return sales.Select(s => new Sales()
            { 
                Id = s.Id,
                Customer = s.Customer,
                ItemId = s.ItemId,
                SalesManager = s.SalesManager,
                PricePerUnit = s.PricePerUnit,
                Quantity = s.Quantity,
                Description = s.Description,
                DateCreated = s.DateCreated

            }).ToList();

        }

         public async Task<Sales> StartSales(CreateSalesRequestModel model)
               {
                   try
                   {
                       var checkAllocatedSalesItem = await _allocateSalesItemToSalesManager.GetAllocatedSalesItem(model.AllocateSalesItemToSalesManagerId);
                       
                       var sales = new Sales
                       {
                           ItemId = model.ItemId,
                           Item = model.Item,
                           CustomerId = model.CustomerId,
                           SalesManagerId = model.SalesManagerId,
                           DateCreated = DateTime.UtcNow,
                           PricePerUnit = model.PricePerUnit,
                           Quantity = model.Quantity,
                           Description = model.Description,
                           
                       };
                       var pricePerUnit = sales.PricePerUnit;
                       var quantity = sales.Quantity;
                       var salesItem = new SalesItem
                       {
                           SalesId = sales.Id,
                           Sales = sales,
                           DateCreated = DateTime.UtcNow,
                       };
                       if (sales.Quantity<=checkAllocatedSalesItem.QuantityAllocated)
                       {
                           sales.TotalPrice += pricePerUnit * quantity;
                           sales.SalesItems.Add(salesItem);
                           checkAllocatedSalesItem.QuantityAllocated = checkAllocatedSalesItem.QuantityAllocated - sales.Quantity;
                        
                           await _allocateSalesItemToSalesManager.UpdateAllocatedSalesItem(model.AllocateSalesItemToSalesManagerId, checkAllocatedSalesItem);
                           await _salesRepository.CreateSales(sales);
                           return sales;
                       }
                       else
                       {
                           throw new Exception("The quantity available is not enough");
                       }
                       
                   }
                   catch
                   {
         
                       throw new Exception("Sales cannot be completed");
                   }
                
                   
                   
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
                   Quantity = sales.Quantity,
                   PricePerUnit = sales.PricePerUnit,
                   TotalPrice = sales.Quantity * sales.PricePerUnit,

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

        public async Task<BaseResponse<ReturnGoodsDto>> ReturnGoods(ReturnGoodsRequestModel model)
        {
            //var checkSalesItem = await _salesRepository.FindSalesItemById(model.SalesItemId);
            var sales = await _salesRepository.FindSalesById(model.SalesId);
            if (sales==null)
            {
                return new BaseResponse<ReturnGoodsDto>
                {
                    Message = "Sales not found!",
                    Status = false
                };
            }
            
            if (DateTime.UtcNow > sales.DateCreated.AddDays(7))
            {
                return new BaseResponse<ReturnGoodsDto>
                {
                    Message = "The time interval for returning goods has elapsed",
                    Status = false
                };
            }
            else if (DateTime.UtcNow <= sales.DateCreated.AddDays(7))
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
                sales.Quantity = sales.Quantity - returnGoods.QuantityReturned;
                sales.TotalPrice = sales.Quantity * sales.PricePerUnit;
                
                
                await _salesRepository.UpdateSales(sales.Id, sales);
                await _salesRepository.UpdateSales(sales.Id, sales);
                await _returnGoodsRepository.ReturnGoods(returnGoods);


            }
            return new BaseResponse<ReturnGoodsDto>
            {
                Message = "Goods successfully returned",
                Status = true
            };
        }
        
        public async Task<List<Sales>> GenerateInvoice(int id)
        {
            return await _salesRepository.GenerateInvoice(id);
        }

        public JsonResult ManageCustomersPatronage(int customerId)
        {
            var customerIdCount= _salesRepository.ManageCustomersPatronage(customerId);
            return new JsonResult(new {myCutomerIdCount = customerIdCount});
        }
    }
}