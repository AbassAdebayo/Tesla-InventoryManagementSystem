using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<BaseResponse<CategoryDto>> CreateCategory(CreateCategoryRequestModel model)
        {
            try
            {
                var category = await _categoryRepository.CategoryExistsByName(model.CategoryName);
                if (category!=null)
                {
                    return new BaseResponse<CategoryDto>
                    {
                        Message = $"Category name {model.CategoryName} already exists!",
                        Status = false
                    };
                }

                var newCategory = new Category
                {
                    CategoryName = model.CategoryName,
                    Description = model.Description,
                    DateCreated = DateTime.UtcNow
                };
                await _categoryRepository.AddCategory(newCategory);
                return new BaseResponse<CategoryDto>
                {
                    Message = "Category successfully created",
                    Status = true,
                    Data = new CategoryDto
                    {
                        Description = newCategory.Description,
                        CategoryName = newCategory.CategoryName
                    }
                };
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<BaseResponse<CategoryDto>> UpdateCategory(int id, UpdateCategoryRequestModel model)
        {
            try
            {
                var categoryCheck = await _categoryRepository.GetCategoryById(id);
                if (categoryCheck==null)
                {
                    return new BaseResponse<CategoryDto>
                    {
                        Message = "Unsuccessful request",
                        Status = false
                    };
                }

                categoryCheck.Description = model.Description;
                categoryCheck.CategoryName = model.CategoryName;
               await _categoryRepository.UpdateCategory(id, categoryCheck);
                return new BaseResponse<CategoryDto>
                {
                    Message = "Data successfully updated",
                    Status = true,
                    Data = new CategoryDto
                    {
                        Description = categoryCheck.Description,
                        CategoryName = categoryCheck.CategoryName
                    }
                };
            }
            catch 
            {
                throw new Exception();
            }
        }

        public async Task<BaseResponse<CategoryDto>> DeleteCategory(int id)
        {
            try
            {
                var categoryCheck = await _categoryRepository.GetCategoryById(id);
                if (categoryCheck==null)
                {
                    return new BaseResponse<CategoryDto>
                    {
                        Message = "Unsuccessful request",
                        Status = false
                    };
                }

                await _categoryRepository.DeleteCategory(categoryCheck);
                return new BaseResponse<CategoryDto>
                {
                    Message = "Data successfully deleted",
                    Status = true
                };
            }
            catch 
            {
                throw new Exception();
            }
        }

        public async Task<BaseResponse<CategoryDto>> ExistsByName(string categoryName)
        {
            try
            {
                var checkCategory = await _categoryRepository.CategoryExistsByName(categoryName);
                if (checkCategory == null)
                {
                    return new BaseResponse<CategoryDto>
                    {
                        Message = "Category name does not exist",
                        Status = false
                    };
                }

                return new BaseResponse<CategoryDto>
                {
                    Message = "Data successfully fetched!",
                    Status = true,
                    Data = new CategoryDto
                    {
                        Id = checkCategory.Id,
                        Description = checkCategory.Description,
                        CategoryName = checkCategory.CategoryName
                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            try
            {
                var categoryCheck = await _categoryRepository.GetCategoryById(id);
                if (categoryCheck==null)
                {

                    throw new Exception("Request not completed!");

                }
                return new CategoryDto
                {

                    Id = categoryCheck.Id,
                    Description = categoryCheck.Description,
                    CategoryName = categoryCheck.CategoryName,
                    DateCreated = categoryCheck.DateCreated

                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories();
                if (categories==null)
                {
                    throw new Exception("Request unsuccessful!");
                }
                
                return categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    CategoryName = c.CategoryName,
                    DateCreated = c.DateCreated
                }).ToList();

            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}