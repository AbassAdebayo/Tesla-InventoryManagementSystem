using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

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

        public Task<SalesItem> CreateSalesItem(SalesItem salesItem);

        public Task<SalesItem> UpdateSalesItem(int id, SalesItem salesItem);

        public Task<SalesItem> FindSalesItemById(int id);

        public Task<bool> DeleteSalesItem(int id);

        public Task<bool> DeleteSalesItems();

        public Task<IList<SalesItem>> GetAllSalesItems(IEnumerable<int> salesItemIds);
        
        public Task<IList<SalesItem>> GetAllSalesItems();
        
        public Task<SalesItem> GetSalesItemById(int id);
        
        public Task<decimal> GetGrandTotalOfAllSales();
        
        public Task<IList<Sales>> GetSalesItemByDate(DateTime date);

        public Task<List<Sales>> GenerateInvoice(int id);
    }
}