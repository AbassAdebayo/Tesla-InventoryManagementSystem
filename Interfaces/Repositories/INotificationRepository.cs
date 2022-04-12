using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotification(Notification notification);

        Task<Notification> UpdateNotification(int id, Notification notification);

        Task<Notification> GetNotification(int id);

        Task<IList<Notification>> GetAllNotifications();
        
        Task<IList<Notification>> GetAllConfirmedNotifications();
      
        Task<IList<Notification>> GetAllRejectedNotifications();


    }
}