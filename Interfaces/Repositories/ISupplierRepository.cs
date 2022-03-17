using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        public Task<Supplier> AddSupplierAsync(Supplier supplier);
        
        public Task<Supplier> UpdateSupplierAsync(int id, Supplier supplier);
        
        public Task<Supplier> DeleteSupplierAsync(Supplier supplier);
        
        public Task<Supplier> SupplierExistByCompanyNameAsync(string companyName);
        
        public Task<Supplier> GetSupplierByIdAsync(int id);
        
        public Task<IEnumerable<Supplier>> GetAllSuppliers();
    }
}