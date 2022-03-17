using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        
        public string CategoryName { get; set; }
        
        public string Description { get; set; }

        public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();
    }

    public class CreateCategoryRequestModel
    {
        [Required(ErrorMessage = "The field must not be empty!")]
        public string CategoryName { get; set; }
        
        public string Description { get; set; }
    }

    public class UpdateCategoryRequestModel
    {
        [Required(ErrorMessage = "The field must not be empty!")]
        public string CategoryName { get; set; }
        
        public string Description { get; set; }
    }
}