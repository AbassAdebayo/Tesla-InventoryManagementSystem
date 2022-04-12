using System;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagemenSystem_Ims.Controllers
{
    public class NotificationController:Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper, SalesManager")]
        public async Task<IActionResult> Index()
        {
            var notification = await _notificationService.GetAllNotifications();
            string alertMessage = "You currently have no notification";
            
            if (notification==null)
            {
                ViewBag.error = alertMessage;
            }

            TempData["data"] = $"You have new messages {notification}";
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "ShopManager,StockKeeper, SalesManager")]
        public async Task<IActionResult> GetNotification(int id)
        {
            var notification = await _notificationService.GetNotification(id);

            if (notification==null)
            {
                throw new Exception("Notification not found!");
            }
            return View(notification);
        }
        
        
        
        [HttpGet]
        [Authorize(Roles = "SalesManager")]
        public async Task<IActionResult> UpdateNotificationToConfirmed(int id)
        {
            
            await _notificationService.UpdateNotificationToConfirmed(id);
            string alertMessage = "You've successfully confirmed allocation(s)";
            
            TempData["data"] = alertMessage;
            
            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        [Authorize(Roles = "SalesManager")]
        public async Task<IActionResult> UpdateNotificationToRejected(int id)
        {
            await _notificationService.UpdateNotificationToRejected(id);
            string alertMessage = "You've successfully rejected allocation(s)";
            
            TempData["data"] = alertMessage;
            
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager, ShopManager, StockKeeper")]
        public async Task<IActionResult> GetAllConfirmedNotifications()
        {
            var confirmedNotifications = await _notificationService.GetAllConfirmedNotifications();
            return View(confirmedNotifications);
        }
        
        [HttpGet]
        [Authorize(Roles = "SalesManager, ShopManager, StockKeeper")]
        public async Task<IActionResult> GetAllRejectedNotifications()
        {
            var rejectedNotifications = await _notificationService.GetAllRejectedNotifications();
            return View(rejectedNotifications);
        }

    }
}