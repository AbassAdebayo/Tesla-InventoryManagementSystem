using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface ISalesManagerService
    {
        public Task<BaseResponse<bool>> RegisterSalesManager(RegisterSalesManagerRequestModel model);
        
        public Task<BaseResponse<bool>> UpdateSalesManager(int id, UpdateSalesManagerRequestModel model);
        
        public Task<BaseResponse<bool>> DeleteSalesManager(int id);
        
        public Task<SalesManagerDto> GetSalesManagerById(int id);
        
        public Task<IEnumerable<SalesManagerDto>> GetAllSalesManagers();
        
        public Task<SalesManagerDto> GetSalesManagerByUserName(string userName);
        
        
    }
}