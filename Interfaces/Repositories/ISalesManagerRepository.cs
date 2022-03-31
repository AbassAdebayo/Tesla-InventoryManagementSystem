using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface ISalesManagerRepository
    {
        public Task<SalesManager> AddSalesManagerAsync(SalesManager salesManager);
        
        public Task<SalesManager> UpdateSalesManagerAsync(int id, SalesManager salesManager);
        
        public Task<SalesManager> DeleteSalesManagerAsync(SalesManager salesManager);
        
        public Task<SalesManager> GetSalesManagerByUsernameAsync(string userName);
        public Task<SalesManager> GetSalesManagerByEmailAsync(string email);
        
        public Task<SalesManager> GetSalesManagerByIdAsync(int id);
        
        public Task<IEnumerable<SalesManager>> GetAllSalesManagers();
    }
}