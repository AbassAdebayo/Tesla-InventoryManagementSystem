using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class ItemRepository: IItemRepository
    {
        private readonly ImsContext _imsContext;

        public ItemRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }

        public async Task<Item> CreateItem(Item item)
        {
            await _imsContext.Items.AddAsync(item);
            await _imsContext.SaveChangesAsync();
            return item;

        }

        public async Task<Item> UpdateItem(int id, ItemDto item)
        {
          var checkItem= await _imsContext.Items.FindAsync(id);
           _imsContext.Update(checkItem);
          await _imsContext.SaveChangesAsync();
          return checkItem;

        }

        public async Task<bool> DeleteItem(int id)
        {
            var checkItem = await _imsContext.Items.FindAsync(id);
             _imsContext.Items.Remove(checkItem);
            await _imsContext.SaveChangesAsync();
             return true;
        }

        public async Task<Item> ExistsByName(string itemName)
        {
            return await _imsContext.Items.FirstOrDefaultAsync(x => x.ItemName == itemName);
           
        }

        public async Task<ItemDto> GetItemById(int id)
        {
            return await _imsContext.Items.Include(x => x.ItemCategories).ThenInclude(x => x.Category).Where(x => x.Id == id)
                .Select(item => new ItemDto
                {
                    Description = item.Description,
                    Id = item.Id,
                    ItemName = item.ItemName,
                    Categories = item.ItemCategories.Select(c=> new CategoryDto
                    {
                        Description = c.Category.Description,
                        CategoryName = c.Category.CategoryName
                        
                    }).ToList()

                }).SingleOrDefaultAsync();
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _imsContext.Items.ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetSelectedItems(IList<int> Ids)
        {
            return await _imsContext.Items.Where(x => Ids.Contains(x.Id)).ToListAsync();
        }
    }
}