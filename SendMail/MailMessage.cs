using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace InventoryManagemenSystem_Ims.SendMail
{
    public class MailMessage: IMailMessage
    {
        private readonly IStockKeeperService _stockKeeperService;
        private readonly ISalesManagerService _salesManagerService;

        public MailMessage(IStockKeeperService stockKeeperService, ISalesManagerService salesManagerService)
        {
            _stockKeeperService = stockKeeperService;
            _salesManagerService = salesManagerService;
        }
        
        public void SendLowQuantityReminderToEmail(StockItem stockItem, int id)
        {
            var stockKeeper = _stockKeeperService.GetStockKeeperById(id);
            MimeMessage reminder = new MimeMessage();
            reminder.From.Add(new MailboxAddress("Stock-InventoryManagementSystem", "greatmoh007@gmail.com"));
            reminder.To.Add(new MailboxAddress("Stock-InventoryManagementSystem", "greatmoh007@gmail.com"));
            reminder.Cc.Add(new MailboxAddress($"{stockKeeper.Result.FirstName}", $"{stockKeeper.Result.Email}"));
            
            reminder.Subject = "Low Item Quantity";
            reminder.Body = new TextPart("plain")
            {
                Text = $"The Item(s) with the details below has reached LOW LEVEL! Kindly refill the stock to " +
                       $"meet up customers' needs...\nStockItemId: {stockItem.Id}\nItemId: {stockItem.ItemId} Item: {stockItem.Item}\n " +
                       $"PricePerUnit: {stockItem.PricePerUnit}\n Quantity: {stockItem.Quantity}"
                
            };

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("greatmoh007@gmail.com", "Ayodejimoh");
                client.Send(reminder);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        // public void SendItemExpiryDateReminderToEmail(Item item, int id)
        // {
        //    
        //     var stockKeeper = _stockKeeperService.GetStockKeeperById(id);
        //     MimeMessage reminder = new MimeMessage();
        //     reminder.From.Add(new MailboxAddress("Stock-InventoryManagementSystem", "greatmoh007@gmail.com"));
        //     reminder.To.Add(new MailboxAddress("Stock-InventoryManagementSystem", "greatmoh007@gmail.com"));
        //     reminder.Cc.Add(new MailboxAddress($"{stockKeeper.Result.FirstName}", $"{stockKeeper.Result.Email}"));
        //     reminder.Subject = "Item Expiration Reminder";
        //     reminder.Body = new TextPart("plain")
        //     {
        //         Text = $"The Item(s) with the details below will be expired in {item.ExpiryDate - DateTime.UtcNow} Days time! Kindly check the expiry date and do the needful to avoid loss" +
        //                $"...\nId: {item.Id}\nItemName: {item.ItemName} Description: {item.Description}\n " +
        //                $"ExpiryDate: {item.ExpiryDate}"
        //         
        //     };
        //
        //     SmtpClient client = new SmtpClient();
        //
        //     try
        //     {
        //         client.Connect("smtp.gmail.com", 465, true);
        //         client.Authenticate("greatmoh007@gmail.com", "Ayodejimoh");
        //         client.Send(reminder);
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception(e.Message);
        //     }
        //     finally
        //     {
        //         client.Disconnect(true);
        //         client.Dispose();
        //     }
        // }
    }
}