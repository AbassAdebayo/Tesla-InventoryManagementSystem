using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Implementations.Services;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> AddCategory(Category category);
        
        public Task<Category> GetCategoryById(int id);
        
        public Task<Category> DeleteCategory(Category category);

        public Task<Category> UpdateCategory(int id, Category category);
        
        public Task<IList<Category>> GetAllCategories();
        
        public Task<Category>  CategoryExistsByName(string categoryName);
        
        Task<IList<Category>> GetSelectedCategories(IList<int> ids);
    }
}