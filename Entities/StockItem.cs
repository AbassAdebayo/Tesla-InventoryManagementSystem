using System.ComponentModel.DataAnnotations;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class StockItem: BaseEntity
    {
        public string StockName { get; set; }
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
}