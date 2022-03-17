using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class ItemService:IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemService(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }
        
        public async Task<BaseResponse<ItemDto>> CreateItem(CreateItemRequestModel model)
        {
            try
            {
                var item = await _itemRepository.ExistsByName(model.ItemName);
                if (item!=null)
                {
                    return new BaseResponse<ItemDto>
                    {
                        Message = "Item already exist!",
                        Status = false

                    };
                }

                item = new Item
                {
                    Description = model.Description,
                    ItemName = model.ItemName,
                    ExpiryDate = model.ExpiryDate,
                    DateCreated = DateTime.UtcNow

                };
                var categories = await _categoryRepository.GetSelectedCategories(model.Categories);

                foreach (var category in categories)
                {
                    var itemCategory = new ItemCategory
                    {
                        Category = category,
                        categoryId = category.Id,
                        Item = item,
                        ItemId = item.Id
                    };
                    item.ItemCategories.Add(itemCategory);
                }
                await _itemRepository.CreateItem(item);

                return new BaseResponse<ItemDto>
                {
                    Message = "Item created successfully",
                    Status = true,
                    Data = new ItemDto
                    {
                        ItemName = model.ItemName,
                        Description = model.Description,
                        ExpiryDate = model.ExpiryDate,
                        DateCreated = item.DateCreated
                        
                    },
                    
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<ItemDto>> UpdateItem(int Id, UpdateItemRequestModel model)
        {
            
            var item= await _itemRepository.GetItemById(Id);
            if (item!=null)
            {
                item.ItemName = model.ItemName;

            }

            await _itemRepository.UpdateItem(Id, item);
            return new BaseResponse<ItemDto>
            {
                Message = "Item successfully updated",
                Status = true
            };
        }

        public async Task<BaseResponse<ItemDto>> DeleteItem(int Id)
        {
            await _itemRepository.DeleteItem(Id);

            return new BaseResponse<ItemDto>
            {
                Message = "Item successfully deleted",
                Status = true
            };
        }

        public async Task<BaseResponse<Item>> ExistsByName(string itemName)
        {
            try
            {
                var verify= await _itemRepository.ExistsByName(itemName);

                if (verify==null)
                {
                    return new BaseResponse<Item>
                    {
                        Message = "Item cannot be found",
                        Status = false,
                    };
                }

                return new BaseResponse<Item>
                {
                    Message = "Data fetched successfully",
                    Status = true,
                    Data = new Item
                    {
                        ItemName = verify.ItemName,
                        Description = verify.Description,
                        Id = verify.Id,
                        DateCreated = verify.DateCreated,
                        DateModified = verify.DateModified,
                        ExpiryDate = verify.ExpiryDate,

                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<ItemDto>> GetItemById(int Id)
        {
            var itemCheck = await _itemRepository.GetItemById(Id);
            if (itemCheck!=null)
            {
                
                return new BaseResponse<ItemDto>
                {
                    Message = "Stock fetched!",
                    Status = true,
                    Data = new ItemDto
                    {
                        ItemName = itemCheck.ItemName,
                    }
                };
            }
            
            return new BaseResponse<ItemDto>
            {
                Status = false,
                Message = "stock does not exist!"
            };
        }

        public async Task<BaseResponse<IList<ItemDto>>> GetAllItems()
        {
            var items = await _itemRepository.GetAllItems();
            return new BaseResponse<IList<ItemDto>>()
            { 
                Message = "Stocks retrieved successfully",
                Status = true,
                Data = items.Select(i=>new ItemDto
                {
                    ItemName = i.ItemName,
                    Id = i.Id,
                    DateCreated = i.DateCreated,
                    DateModified = i.DateModified,
                    ExpiryDate = i.ExpiryDate
                }).ToList()
            };
        }
    }
}