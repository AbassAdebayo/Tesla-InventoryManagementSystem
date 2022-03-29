using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using InventoryManagemenSystem_Ims.SendMail;
using MailKit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;

namespace InventoryManagemenSystem_Ims.BackgroundTask
{
    public class ReminderMails:BackgroundService
    {
        private readonly ReminderMailsConfig _configuration;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<ReminderMails> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        
        public ReminderMails(IServiceScopeFactory serviceScopeFactory, 
            IOptions<ReminderMailsConfig> configuration, ILogger<ReminderMails> logger)
        {
            
            _configuration = configuration.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _schedule = CrontabSchedule.Parse(_configuration.CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);

        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var salesManagerContext = scope.ServiceProvider.GetRequiredService<ISalesManagerService>();
                    var stockKeeperContext = scope.ServiceProvider.GetRequiredService<IStockKeeperService>();
                    var stockContext = scope.ServiceProvider.GetRequiredService<IStockService>();
                    var itemContext = scope.ServiceProvider.GetRequiredService<IItemService>();
                    var mailContext = scope.ServiceProvider.GetRequiredService<IMailMessage>();
                    var stockItems = await stockContext.GetAllStockItems();
                    var items = await itemContext.GetAllItems();
                    var salesManagers = await salesManagerContext.GetAllSalesManagers();

                    foreach (var stockItem in stockItems)
                    {
                        var stockKeeper = await stockKeeperContext.GetStockKeeperById(stockItem.Id);
                        var itemInStock  = await stockContext.GetStockItemById(stockItem.Id);
                        var newStockItem = new StockItem
                        {
                            PricePerUnit = itemInStock.PricePerUnit,
                            Id = itemInStock.Id,
                            Quantity = itemInStock.Quantity,
                            ItemId = itemInStock.ItemId,
                            Item = itemInStock.Item,
                        };
                        if (stockItem.Quantity==10)
                        {
                            mailContext.SendLowQuantityReminderToEmail(newStockItem, stockKeeper.Id);
                        }
                        

                    }

                    // foreach (var item in items)
                    // {
                    //     var stockKeeper = await stockKeeperContext.GetStockKeeperById(item.Id);
                    //   var newItem = new Item
                    //     {
                    //         Id = item.Id,
                    //         ItemName = item.ItemName,
                    //         Description = item.Description,
                    //         ExpiryDate = item.ExpiryDate
                    //     };
                    //     //if (DateTime.Today.AddMonths(1)== item.ExpiryDate)
                    //     if (DateTime.UtcNow>item.ExpiryDate)
                    //     {
                    //         mailContext.SendItemExpiryDateReminderToEmail(newItem, stockKeeper.Id);
                    //     }
                    //}
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error occured while implementing reminder. {e.Message}");
                    _logger.LogError(e, e.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(ReminderMails)} is stopping ");
                var timeSpan = _nextRun - now;
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow.AddHours(5));
            }
        }
    }
}