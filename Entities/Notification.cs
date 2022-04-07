using InventoryManagemenSystem_Ims.Enums;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Notification: BaseEntity
    {
        public int AllocateSalesItemToSalesManagerId { get; set; }
        
        public AllocateSalesItemToSalesManager AllocateSalesItemToSalesManager { get; set; }
        
        public NotificationStatus NotificationStatus { get; set; }
    }
}