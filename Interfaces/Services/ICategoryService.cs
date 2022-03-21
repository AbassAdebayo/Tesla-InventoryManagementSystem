using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryDto>> CreateCategory(CreateCategoryRequestModel model);

        Task<BaseResponse<CategoryDto>> UpdateCategory(int id, UpdateCategoryRequestModel model);

        Task<BaseResponse<CategoryDto>> DeleteCategory(int id);

        Task<BaseResponse<CategoryDto>> ExistsByName(string categoryName);

        Task<CategoryDto> GetCategoryById(int id);

        Task<IList<CategoryDto>> GetAllCategories();
    }
}