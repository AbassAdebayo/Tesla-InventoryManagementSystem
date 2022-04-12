using System;
using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class AllocateSalesItemToSalesManagerDto
    {
        public int Id { get; set; }
        
        public int StockKeeperId { get; set; }
        
        public string StockName { get; set; }
        
        public StockKeeper StockKeeper { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public int QuantityAllocated { get; set; }
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        
        //public int AllocateSalesItemToSalesManagerId { get; set; }
        
        //public AllocateSalesItemToSalesManager AllocateSalesItemToSalesManager { get; set; }
    }

    public class CreateAllocationResponseModel
    {
        public int StockItemId { get; set; }
        
        public string StockName { get; set; }
        
        public int StockKeeperId { get; set; }
        
        public StockKeeper StockKeeper { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public int QuantityAllocated { get; set; }
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
    }
    
   
}