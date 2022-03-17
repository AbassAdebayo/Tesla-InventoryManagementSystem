using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly ImsContext _imsContext;

        public CustomerRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _imsContext.Customers.AddAsync(customer);
            await _imsContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            _imsContext.Customers.Update(customer);
            await _imsContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomerAsync(Customer customer)
        {
            _imsContext.Customers.Remove(customer);
            await _imsContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> CustomerExistByCompanyNameAsync(string companyName)
        {
            return await _imsContext.Customers.FirstOrDefaultAsync(c => c.ShopName == companyName);
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _imsContext.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _imsContext.Customers.ToListAsync();
        }
    }
}