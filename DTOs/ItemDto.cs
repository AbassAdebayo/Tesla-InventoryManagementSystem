using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        
        public bool IsDeleted { get; set; }
        public string ItemName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        
        public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
    }

    public class CreateItemRequestModel
    {
        [Required]
        public string ItemName { get; set; }
        
        public string Description { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime DateCreated { get; set; }
        
        [Required]
        public DateTime ExpiryDate { get; set; }
        
        [Required]
        public IList<int> Categories { get; set; } = new List<int>();
    }
    
    public class UpdateItemRequestModel
    {
        public string ItemName { get; set; }
        
        public string Description { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime DateModified { get; set; }
        
    }
}