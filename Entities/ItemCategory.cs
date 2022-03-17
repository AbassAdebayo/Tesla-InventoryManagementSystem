namespace InventoryManagemenSystem_Ims.Entities
{
    public class ItemCategory: BaseEntity
    {
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
        public int categoryId { get; set; }
        
        public Category Category { get; set; }
        
    }
}