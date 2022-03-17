using System;
using InventoryManagemenSystem_Ims.Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace InventoryManagemenSystem_Ims.SendMail
{
    public class MailMessage: IMailMessage
    {
        public void SendEmailAddressFromSalesManager(string recipient, string content, string subject)
        {
            MimeMessage message = new MimeMessage();
            
            message.From.Add(new MailboxAddress("Sales-InventoryManagementSystem", "greatmoh007@gmail.com"));
            message.To.Add(MailboxAddress.Parse(recipient));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = content
            };

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("greatmoh007@gmail.com", "Ayodejimoh");
                client.Send(message);
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

        public void SendEmailAddressFromStockKeeper(string recipient, string subject, string content)
        {
            MimeMessage stockKeepermessage = new MimeMessage();
            
            stockKeepermessage.From.Add(new MailboxAddress("Stock-InventoryManagementSystem", "greatmoh007@gmail.com"));
            stockKeepermessage.To.Add(MailboxAddress.Parse(recipient));
            stockKeepermessage.Subject = subject;
            stockKeepermessage.Body = new TextPart("plain")
            {
                Text = content
            };

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("greatmoh007@gmail.com", "Ayodejimoh");
                client.Send(stockKeepermessage);
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
    }
}