using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        public Task<Customer> CreateCustomerAsync(Customer customer);
        
        public Task<Customer> UpdateCustomerAsync(int id, Customer customer);
        
        public Task<Customer> DeleteCustomerAsync(Customer customer);
        
        public Task<Customer> CustomerExistByCompanyNameAsync(string companyName);
        
        public Task<Customer> GetCustomerByIdAsync(int id);
        
        public Task<IEnumerable<Customer>> GetAllCustomers();
    }
}