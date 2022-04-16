using System;
using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Enums;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        
        public bool IsDeleted { get; set; }
        public int AllocateSalesItemToSalesManagerId { get; set; }
        
        public AllocateSalesItemToSalesManager AllocateSalesItemToSalesManager { get; set; }
        
        public NotificationStatus NotificationStatus { get; set; }
    }
}