using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface ISalesService
    {

        public Task<BaseResponse<SalesDto>> FindSalesById(int id);

        public Task<Sales> UpdateSales(UpdateSalesRequestModel model);

       // public Task<bool> DeleteSales(int id, int itemId);
        
        
        public Task<IEnumerable<Sales>> GetAllSales();
        
        public Task<Sales> StartSales(CreateSalesRequestModel model);
        
        public Task<BaseResponse<IList<SalesDto>>> GetSalesItemByDate(DateTime date);
        
        public Task<BaseResponse<decimal>> GetGrandTotalOfAllSales();
        
        public Task<BaseResponse<ReturnGoodsDto>> ReturnGoods(ReturnGoodsRequestModel model);
        
        public Task<List<Sales>> GenerateInvoice(int id);
        
        public JsonResult ManageCustomersPatronage(int customerId);

       public IList<Sales> GetSalesByMonth(DateTime date);
    }
}