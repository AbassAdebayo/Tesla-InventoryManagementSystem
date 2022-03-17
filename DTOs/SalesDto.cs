using System.Collections.Generic;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class SalesDto: BaseEntity
    {
        public int CustomerId { get; set; }
        
        public int SalesManagerId { get; set; }
        public string Description { get; set; }

        public ICollection<CheckOutSales> SalesItems { get; set; } = new List<CheckOutSales>();
        public decimal TotalPrice { get; set; }
        
    }

    public class CreateSalesRequestModel
    {
        public int CustomerId { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public string Description { get; set; }
        public ICollection<CheckOutSales> SalesItems { get; set; } = new List<CheckOutSales>();
        
        public int StockItemId { get; set; }
        
        public string CustomerEmailAddress { get; set; }
        
    }
    
    public class UpdateSalesRequestModel:BaseEntity
    {
        public int CustomerId { get; set; }

        
        public string Description { get; set; }

       
    }

    public class UpdateSalesItemRequestModel
    {
        public int SalesItemId { get; set; }
        
        public int SalesId { get; set; }

        public int ItemId { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal PricePerUnit { get; set; }
    }
}