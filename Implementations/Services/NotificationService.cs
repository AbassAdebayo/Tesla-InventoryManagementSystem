using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<Notification> CreateNotification(Notification notification)
        {
            try
            {
                var newNotification = new Notification
                {
                    AllocateSalesItemToSalesManagerId = notification.AllocateSalesItemToSalesManagerId,
                    AllocateSalesItemToSalesManager = notification.AllocateSalesItemToSalesManager
                };
                await _notificationRepository.CreateNotification(newNotification);
                return newNotification;
            }
            catch
            {

                throw new Exception("Notification unsuccessfully created");
            }
        }

        public async Task<bool> UpdateNotificationToConfirmed(int id)
        {
            try
            {
                var getNotification = await _notificationRepository.GetNotification(id);
                getNotification.NotificationStatus = Enums.NotificationStatus.Confirmed;
                await _notificationRepository.UpdateNotification(getNotification.Id, getNotification);
            }
            catch
            {

                throw new Exception("Notification does not exist!");
            }

            return true;
        }

        public async Task<bool> UpdateNotificationToRejected(int id)
        {
            try
            {
                var getNotification = await _notificationRepository.GetNotification(id);
                getNotification.NotificationStatus = Enums.NotificationStatus.Rejected;
                await _notificationRepository.UpdateNotification(getNotification.Id, getNotification);
            }
            catch
            {

                throw new Exception("Notification does not exist!");
            }

            return true;
        }

        public async Task<Notification> GetNotification(int id)
        {
           return await _notificationRepository.GetNotification(id);
        }

        public async Task<IList<Notification>> GetAllNotifications()
        {
            return await _notificationRepository.GetAllNotifications();
        }
    }
}