using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using Microsoft.VisualBasic.CompilerServices;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface ISalesItemService
    {
        
        public Task<SalesItem> FindSalesItemById(int id);

        public Task<BaseResponse<bool>> DeleteSalesItem(int id, int stockItemId);
        
        public Task<BaseResponse<bool>> DeleteSalesItems();
        
        //public Task<IList<SalesItem>> GetAllSalesItems(IList<int> salesItemIds);
        public Task<List<SalesItem>> GetAllSalesItems();

        //public Task<IEnumerable<SalesItem>> GetAllSalesItemBySalesManagerId(int salesManagerId);


    }
}