using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class SalesManagerRepository:ISalesManagerRepository
    {
        private readonly ImsContext _imsContext;

        public SalesManagerRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<SalesManager> AddSalesManagerAsync(SalesManager salesManager)
        {
            await _imsContext.SalesManagers.AddAsync(salesManager);
            await _imsContext.SaveChangesAsync();
            return salesManager;
        }

        public async Task<SalesManager> UpdateSalesManagerAsync(int id, SalesManager salesManager)
        {
            _imsContext.SalesManagers.Update(salesManager);
            await _imsContext.SaveChangesAsync();
            return salesManager;

        }

        public async Task<SalesManager> DeleteSalesManagerAsync(SalesManager salesManager)
        {
            _imsContext.SalesManagers.Remove(salesManager);
            await _imsContext.SaveChangesAsync();
            return salesManager;
        }

        public async Task<SalesManager> GetSalesManagerByUsernameAsync(string userName)
        {
            return await _imsContext.SalesManagers.FirstOrDefaultAsync(u => u.User.UserName == userName);
        }

        public async Task<SalesManager> GetSalesManagerByIdAsync(int id)
        {
            return await _imsContext.SalesManagers.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IEnumerable<SalesManager>> GetAllSalesManagers()
        {
            return await _imsContext.SalesManagers.ToListAsync();
        }
    }
}