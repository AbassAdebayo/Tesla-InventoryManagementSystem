using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class AllocateSalesItemToSalesManagerRepository:IAllocateSalesItemToSalesManagerRepository
    {
        private readonly ImsContext _imsContext;

        public AllocateSalesItemToSalesManagerRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<AllocateSalesItemToSalesManager> AllocateSalesItem(AllocateSalesItemToSalesManager allocateSalesItemToSalesManager)
        {
            await _imsContext.AllocateSalesItemToSalesManagers.AddAsync(allocateSalesItemToSalesManager);
            await _imsContext.SaveChangesAsync();
            return allocateSalesItemToSalesManager;
        }

        public async Task<bool> DeleteAllocatedSalesItem(AllocateSalesItemToSalesManager allocateSalesItemToSalesManager)
        {
             _imsContext.AllocateSalesItemToSalesManagers.Remove(allocateSalesItemToSalesManager);
             await _imsContext.SaveChangesAsync();
             return true;
        }

        public async Task<IList<AllocateSalesItemToSalesManager>> GetAllAllocatedSalesItem()
        {
            return await _imsContext.AllocateSalesItemToSalesManagers.Include(x => x.Item).Include(x => x.SalesManager).
                Include(x=>x.StockKeeper)
                .Select(allocate => new AllocateSalesItemToSalesManager
                {
                    Id = allocate.Id,
                    Item = allocate.Item,
                     QuantityAllocated = allocate.QuantityAllocated,
                     SalesManager = allocate.SalesManager,
                     StockKeeper = allocate.StockKeeper,
                     DateCreated = allocate.DateCreated

                }).ToListAsync();
        }

        public async Task<AllocateSalesItemToSalesManager> GetAllocatedSalesItem(int id)
        {
            return await _imsContext.AllocateSalesItemToSalesManagers.Include(x => x.Item).Include(x => x.SalesManager)
                .Include(x => x.StockKeeper)
                .Where(x => x.Id == id).Select(allocate => new AllocateSalesItemToSalesManager
                {
                    Id = allocate.Id,
                    Item = allocate.Item,
                    QuantityAllocated = allocate.QuantityAllocated,
                    SalesManager = allocate.SalesManager,
                    StockKeeper = allocate.StockKeeper,
                    DateCreated = allocate.DateCreated

                }).SingleAsync();

            
        }

        public async Task<AllocateSalesItemToSalesManager> UpdateAllocatedSalesItem(int id, AllocateSalesItemToSalesManager allocateSalesItemToSalesManager)
        {
             _imsContext.AllocateSalesItemToSalesManagers.Update(allocateSalesItemToSalesManager);
             await _imsContext.SaveChangesAsync();
             return allocateSalesItemToSalesManager;

        }

        public async Task<AllocateSalesItemToSalesManager> GetAllocatedItemsByItemId(int itemId)
        {
          return await _imsContext.AllocateSalesItemToSalesManagers.FirstOrDefaultAsync(x => x.ItemId == itemId);
        } 
    }
}