using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IStockKeeperService
    {
        public Task<BaseResponse<bool>> RegisterStockKeeper(RegisterStockKeeperRequestModel model);
        
        public Task<BaseResponse<bool>> UpdateStockKeeper(int id, UpdateStockKeeperRequestModel model);
        
        public Task<BaseResponse<bool>> DeleteStockKeeper(int id);
        
        public Task<BaseResponse<StockKeeperDto>> GetStockKeeperById(int id);
        
        public Task<BaseResponse<IList<StockKeeperDto>>> GetAllStockKeepers();
    }
}