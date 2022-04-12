using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Enums;
using InventoryManagemenSystem_Ims.Implementations.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class AllocateSalesItemToSalesManagerService:IAllocateSalesItemToSalesManagerService
    {
        private readonly IAllocateSalesItemToSalesManagerRepository _allocateSalesItemToSalesManager;
        private readonly IStockRepository _stockRepository;
        private readonly INotificationRepository _notificationRepository;

        public AllocateSalesItemToSalesManagerService(IAllocateSalesItemToSalesManagerRepository 
            allocateSalesItemToSalesManager, IStockRepository stockRepository, INotificationRepository notificationRepository)
        {
            _allocateSalesItemToSalesManager = allocateSalesItemToSalesManager;
            _stockRepository = stockRepository;
            _notificationRepository = notificationRepository;
        }
        public async Task<AllocateSalesItemToSalesManagerDto> AllocateSalesItem(CreateAllocationResponseModel model)
        {
            var stockItem = await _stockRepository.GetStockItemById(model.StockItemId);

            var newAllocatedItem = new AllocateSalesItemToSalesManager
            {
                ItemId = model.ItemId,
                Item = model.Item,
                SalesManager = model.SalesManager,
                SalesManagerId = model.SalesManagerId,
                StockKeeperId = model.StockKeeperId,
                StockKeeper = model.StockKeeper,
                QuantityAllocated = model.QuantityAllocated,
                DateCreated = DateTime.UtcNow
            };
            
            var itemForSales = await _allocateSalesItemToSalesManager.AllocateSalesItem(newAllocatedItem);
            if (stockItem.Quantity > newAllocatedItem.QuantityAllocated)
            { 
               
                var newNotification = new Notification
                {
                    AllocateSalesItemToSalesManager = itemForSales,
                    Id = itemForSales.Id,
                    DateCreated = DateTime.UtcNow
                };
                await _notificationRepository.CreateNotification(newNotification);
            
                if(newNotification.NotificationStatus==NotificationStatus.Confirmed)
                {
                    stockItem.Quantity = stockItem.Quantity - newAllocatedItem.QuantityAllocated;
                    await _stockRepository.UpdateStockItem(stockItem.Id, stockItem);
                }
                else if (newNotification.NotificationStatus==NotificationStatus.Rejected)
                {
                    await _allocateSalesItemToSalesManager.DeleteAllocatedSalesItem(newAllocatedItem);
                }

                return new AllocateSalesItemToSalesManagerDto
                {
                    Id = newAllocatedItem.Id,
                    ItemId = newAllocatedItem.ItemId,
                    Item = newAllocatedItem.Item,
                    SalesManager = newAllocatedItem.SalesManager,
                    SalesManagerId = newAllocatedItem.SalesManagerId,
                    StockKeeperId = newAllocatedItem.StockKeeperId,
                    StockKeeper = newAllocatedItem.StockKeeper,
                    QuantityAllocated = newAllocatedItem.QuantityAllocated,
                    DateCreated = DateTime.UtcNow
                };
            }
            else
            {
                throw new Exception("Insufficient Stock!");
            }


        }

        public async Task<bool> DeleteAllocatedSalesItem(AllocateSalesItemToSalesManager allocateSalesItemToSalesManager)
        {
            await _allocateSalesItemToSalesManager.DeleteAllocatedSalesItem(allocateSalesItemToSalesManager);
            return true;
        }

        public async Task<IList<AllocateSalesItemToSalesManager>> GetAllAllocatedSalesItem()
        {
            return await _allocateSalesItemToSalesManager.GetAllAllocatedSalesItem();
        }

        public async Task<AllocateSalesItemToSalesManager> GetAllocatedSalesItem(int id)
        {
            var getAllocatedItem = await _allocateSalesItemToSalesManager.GetAllocatedSalesItem(id);

            if (getAllocatedItem!=null)
            {
                return getAllocatedItem;
            }

            throw new Exception("Allocation not found!");
        }
    }
}