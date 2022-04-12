using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Enums;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly ImsContext _imsContext;

        public NotificationRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<Notification> CreateNotification(Notification notification)
        {
            await _imsContext.Notifications.AddAsync(notification);
            await _imsContext.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> UpdateNotification(int id, Notification notification)
        {
            _imsContext.Notifications.Update(notification);
            await _imsContext.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> GetNotification(int id)
        {
           return await _imsContext.Notifications.Include(x => x.AllocateSalesItemToSalesManager).Where(x => x.Id == id).Select(
                notification => new Notification
                {
                    AllocateSalesItemToSalesManager = notification.AllocateSalesItemToSalesManager,
                    Id = notification.Id,
                    DateCreated = notification.DateCreated
                }).SingleOrDefaultAsync();
        }

        public async Task<IList<Notification>> GetAllNotifications()
        {
            return await _imsContext.Notifications.Include(x=>x.AllocateSalesItemToSalesManager).Select(
                notification => new Notification
                {
                    AllocateSalesItemToSalesManager = notification.AllocateSalesItemToSalesManager,
                    Id = notification.Id,
                    DateCreated = notification.DateCreated
                }).ToListAsync();
        }
        
        
         public async Task<IList<Notification>> GetAllConfirmedNotifications()
         {
             return await _imsContext.Notifications.Include(x => x.AllocateSalesItemToSalesManager)
                 .Where(s => s.NotificationStatus == NotificationStatus.Confirmed).Select(
                     notification => new Notification
                     {
                         Id = notification.Id,
                         AllocateSalesItemToSalesManager = notification.AllocateSalesItemToSalesManager,
                         DateCreated = notification.DateCreated,
                         NotificationStatus = NotificationStatus.Confirmed
                         
                     }).ToListAsync();
         }

         public async Task<IList<Notification>> GetAllRejectedNotifications()
         {
             return await _imsContext.Notifications.Include(x => x.AllocateSalesItemToSalesManager)
                 .Where(s => s.NotificationStatus == NotificationStatus.Rejected).Select(
                     notification => new Notification
                     {
                         Id = notification.Id,
                         AllocateSalesItemToSalesManager = notification.AllocateSalesItemToSalesManager,
                         DateCreated = notification.DateCreated,
                         NotificationStatus = NotificationStatus.Rejected
                         
                     }).ToListAsync();
         }
     }
    }