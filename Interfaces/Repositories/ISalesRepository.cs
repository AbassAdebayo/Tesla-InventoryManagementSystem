using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface ISalesRepository
    {
        public Task<Sales> CreateSales(Sales sales);

        public Task<Sales> FindSalesById(int id);

        public Task<Sales> UpdateSales(int id, Sales sales);

        public Task<bool> DeleteSales(int id);

        public Task<bool> ExistsById(int id);

        public Task<IEnumerable<Sales>> GetAllSales();
        
        public Task<Sales> FindSalesByItemId(int itemId);

        public Task<bool> DeleteSalesItem(int id);

        
        public Task<IList<SalesItem>> GetAllSalesItems();
        
        
        public Task<decimal> GetGrandTotalOfAllSales();
        
        public Task<IList<Sales>> GetSalesItemByDate(DateTime date);

        public Task<List<Sales>> GenerateInvoice(int id);

        public JsonResult ManageCustomersPatronage(int customerId);
        
        
        public IList<Sales> GetSalesByMonth(DateTime date);
    }
}