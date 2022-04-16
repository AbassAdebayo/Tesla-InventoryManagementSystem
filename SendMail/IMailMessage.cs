using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.SendMail
{
    public interface IMailMessage
    {

        public void SendLowQuantityReminderToEmail(StockItem stockItem, int id);
        //public void SendItemExpiryDateReminderToEmail(Item item, int id);
    }
}