using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IItemService
    {
        Task<BaseResponse<ItemDto>> CreateItem(CreateItemRequestModel model);

        Task<BaseResponse<ItemDto>> UpdateItem(int id, UpdateItemRequestModel model);

        Task<BaseResponse<ItemDto>> DeleteItem(int id);

        Task<BaseResponse<Item>> ExistsByName(string stockName);

        Task<ItemDto> GetItemById(int id);

        Task<IList<ItemDto>> GetAllItems();
    }
}