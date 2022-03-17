using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface ISalesService
    {

        public Task<BaseResponse<SalesDto>> FindSalesById(int id);

        public Task<BaseResponse<SalesDto>> UpdateSales(int id, Sales sales);

        public Task<bool> DeleteSales(int id);

        public Task<BaseResponse<bool>> ExistsById(int id);

        public Task<BaseResponse<IEnumerable<SalesDto>>> GetAllSales();

        public Task<BaseResponse<IList<Sales>>> SalesItem(CreateSalesRequestModel model);
        
        public Task<BaseResponse<IList<SalesDto>>> GetSalesItemByDate(DateTime date);
        
        public Task<BaseResponse<decimal>> GetGrandTotalOfAllSales();
        
        public Task<BaseResponse<ReturnGoodsDto>> ReturnGoods(int salesItemId, ReturnGoodsRequestModel model);
    }
}