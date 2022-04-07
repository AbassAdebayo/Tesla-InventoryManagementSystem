using System;
using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Item: BaseEntity
    {
        public string ItemName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        //public int TotalQuantity { get; set; }

        public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();

        public ICollection<ItemCategory> ItemCategories { get; set; } = new List<ItemCategory>();

    }
}