using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IShopManagerService
    {
        public Task<BaseResponse<ShopManagerDto>> RegisterShopManager(RegisterShopManagerRequestModel model);
        
        public Task<BaseResponse<ShopManagerDto>> UpdateShopManager(int id, UpdateShopManagerRequestModel model);
        
        public Task<BaseResponse<bool>> DeleteShopManager(int id);
        
        public Task<BaseResponse<ShopManagerDto>> GetShopManagerById(int id);
        
        public Task<BaseResponse<IList<ShopManagerDto>>> GetAllShopManagers();
    }
}