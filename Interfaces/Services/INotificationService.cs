using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface INotificationService
    {
        Task<Notification> CreateNotification(Notification notification);

        Task<bool> UpdateNotificationToConfirmed(int id);
        
        Task<bool> UpdateNotificationToRejected(int id);

        Task<Notification> GetNotification(int id);

        Task<IList<Notification>> GetAllNotifications();
    }
}