using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class StockDto:BaseEntity
    {
        public string StockName { get; set; }
        
        public string Description { get; set; }
        
        public int SupplierId { get; set; }
        
        public Supplier Supplier { get; set; }
        
        public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
        public int StockId { get; set; }
        
        public Stock Stock { get; set; }
        
        public int Quantity { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal PricePerUnit { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
        
    }

    public class CreateStockRequestModel
    {
        public string StockName { get; set; }
        
        public string Description { get; set; }
        
        public int SupplierId { get; set; }
        
        public IList<int> Items { get; set; } = new List<int>();

    }

    public class UpdateStockRequestModel
    {
        public string StockName { get; set; }
        
        public string Description { get; set; }
        
        
    }

    public class AddItemToStockRequestModel
    {
        public int ItemId { get; set; }
        
        public int StockId { get; set; }
        
        public decimal PricePerUnit { get; set; }
        
        public int Quantity { get; set; }
        
        
    }
    
    public class UpdateStockItemRequestModel
    {
        public int StockItemId { get; set; }
        
        public int ItemId { get; set; }
        
        public int StockId { get; set; }
        
        public decimal PricePerUnit { get; set; }
        
        public int Quantity { get; set; }
        
        
    }
}
