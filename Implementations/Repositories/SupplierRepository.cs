using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class SupplierRepository:ISupplierRepository
    {
        private readonly ImsContext _imsContext;

        public SupplierRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            await _imsContext.Suppliers.AddAsync(supplier);
            await _imsContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> UpdateSupplierAsync(int id, Supplier supplier)
        {
            _imsContext.Suppliers.Update(supplier);
            await _imsContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> DeleteSupplierAsync(Supplier supplier)
        {
            _imsContext.Suppliers.Remove(supplier);
            await _imsContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> SupplierExistByCompanyNameAsync(string companyName)
        {
            return await _imsContext.Suppliers.FirstOrDefaultAsync(u => u.CompanyName == companyName);
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _imsContext.Suppliers.FindAsync(id);
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            return await _imsContext.Suppliers.ToListAsync();
        }
    }
}