using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class SalesDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        
        public int SalesManagerId { get; set; }
        public string Description { get; set; }
        
        
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }

        //public ICollection<CheckOutSales> SalesItems { get; set; } = new List<CheckOutSales>();
        public decimal TotalPrice { get; set; }
        
        public int Quantity { get; set; }
              
        public decimal PricePerUnit { get; set; }
        
        public int SalesId { get; set; }

        public int ItemId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
    }

    public class CreateSalesRequestModel
    {
        public int CustomerId { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public string Description { get; set; }
        //public ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
        
        public int StockItemId { get; set; }
        
        public int Quantity { get; set; }
              
        public decimal PricePerUnit { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        public int AllocateSalesItemToSalesManagerId { get; set; }
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
    }
    

    public class UpdateSalesRequestModel
    {
        public int SalesItemId { get; set; }
        
        public int SalesId { get; set; }

        public int ItemId { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal PricePerUnit { get; set; }
    }
    
}