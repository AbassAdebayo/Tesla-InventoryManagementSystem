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
        private readonly INotificationService _notificationService;

        public AllocateSalesItemToSalesManagerService(IAllocateSalesItemToSalesManagerRepository allocateSalesItemToSalesManager, IStockRepository stockRepository, INotificationRepository notificationRepository, INotificationService notificationService)
        {
            _allocateSalesItemToSalesManager = allocateSalesItemToSalesManager;
            _stockRepository = stockRepository;
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
        }
        public async Task<AllocateSalesItemToSalesManagerDto> AllocateSalesItem(CreateAllocationResponseModel model)
        {
            var stockItem = await _stockRepository.GetStockItemsByItemId(model.ItemId);

            var allocationExistenceCheck = await _allocateSalesItemToSalesManager.GetAllocatedItemsByItemId(model.ItemId);

            if (allocationExistenceCheck==null)
            {

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
            
                if (stockItem.Quantity > itemForSales.QuantityAllocated)
                {
                    var newNotification = new Notification
                    {
                        AllocateSalesItemToSalesManager = itemForSales,
                        Id = itemForSales.Id,
                        DateCreated = DateTime.UtcNow
                    };
                    await _notificationRepository.CreateNotification(newNotification);
                
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
            else
            {
                allocationExistenceCheck.QuantityAllocated += model.QuantityAllocated;
                 await _allocateSalesItemToSalesManager.UpdateAllocatedSalesItem(allocationExistenceCheck.Id,
                    allocationExistenceCheck);
                
                var newNotification = new Notification
                {
                    AllocateSalesItemToSalesManager = allocationExistenceCheck,
                    Id = allocationExistenceCheck.Id,
                    DateCreated = DateTime.UtcNow
                };
                await _notificationRepository.UpdateNotification(newNotification.Id, newNotification);
            }

            return new AllocateSalesItemToSalesManagerDto
            {
                Id = allocationExistenceCheck.Id,
                ItemId = allocationExistenceCheck.ItemId,
                Item = allocationExistenceCheck.Item,
                SalesManager = allocationExistenceCheck.SalesManager,
                SalesManagerId = allocationExistenceCheck.SalesManagerId,
                StockKeeperId = allocationExistenceCheck.StockKeeperId,
                StockKeeper = allocationExistenceCheck.StockKeeper,
                QuantityAllocated = allocationExistenceCheck.QuantityAllocated +model.QuantityAllocated,
                DateCreated = DateTime.UtcNow
            };
            
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