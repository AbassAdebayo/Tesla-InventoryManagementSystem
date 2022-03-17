namespace InventoryManagemenSystem_Ims.SendMail
{
    public interface IMailMessage
    {
        public void SendEmailAddressFromSalesManager(string recipient, string subject, string content);
        
        public void SendEmailAddressFromStockKeeper(string recipient, string subject, string content);
    }
}