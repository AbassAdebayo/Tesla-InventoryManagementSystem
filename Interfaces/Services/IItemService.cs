using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IItemService
    {
        Task<BaseResponse<ItemDto>> CreateItem(CreateItemRequestModel model);

        Task<BaseResponse<ItemDto>> UpdateItem(int Id, UpdateItemRequestModel model);

        Task<BaseResponse<ItemDto>> DeleteItem(int Id);

        Task<BaseResponse<Item>> ExistsByName(string stockName);

        Task<BaseResponse<ItemDto>> GetItemById(int Id);

        Task<BaseResponse<IList<ItemDto>>> GetAllItems();
    }
}