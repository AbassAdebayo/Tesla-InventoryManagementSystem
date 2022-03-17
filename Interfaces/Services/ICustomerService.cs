using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface ICustomerService
    {
        public Task<BaseResponse<bool>> CreateCustomer(CreateCustomerRequestModel model);
        
        public Task<BaseResponse<bool>> UpdateCustomer(int id, UpdateCustomerRequestModel model);
        
        public Task<BaseResponse<bool>> DeleteCustomer(int id);
        
        public Task<BaseResponse<CustomerDto>> GetCustomerById(int id);
        
        public Task<BaseResponse<IList<CustomerDto>>> GetAllCustomers();
        
        public Task<BaseResponse<CustomerDto>> CustomerExistsByShopName(string shopName);
    }
}