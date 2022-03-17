using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagemenSystem_Ims.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        
        public bool IsDeleted { get; set; }

    }
}