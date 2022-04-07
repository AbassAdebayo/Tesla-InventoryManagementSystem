using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IItemRepository
    {
        Task<Item> CreateItem(Item item);

        Task<Item> UpdateItem(int id, ItemDto item);

        Task<bool> DeleteItem(int id);

        Task<Item> ExistsByName(string itemName);

        Task<ItemDto> GetItemById(int id);

        Task<IList<Item>> GetAllItems();
        
        Task<IEnumerable<Item>> GetSelectedItems(IList<int> Ids);
    }
}