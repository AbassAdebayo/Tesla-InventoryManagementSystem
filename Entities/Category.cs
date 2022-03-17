using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Category: BaseEntity
    {
        public string CategoryName { get; set; }
        
        public string Description { get; set; }

        public ICollection<ItemCategory> ItemCategories { get; set; } = new List<ItemCategory>();
    }
}