using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ImsContext _imsContext;

        public CategoryRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<Category> AddCategory(Category category)
        {
            await _imsContext.Categories.AddAsync(category);
            await _imsContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _imsContext.Categories.FindAsync(id);
            
        }

        public async Task<Category> DeleteCategory(Category category)
        {
            _imsContext.Categories.Remove(category);
            await _imsContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategory(int id, Category category)
        {
            _imsContext.Categories.Update(category);
            await _imsContext.SaveChangesAsync();
            return category;
        }

        public async Task<IList<Category>> GetAllCategories()
        {
            return await _imsContext.Categories.ToListAsync();
        }

        public async Task<Category> CategoryExistsByName(string categoryName)
        {
            return await _imsContext.Categories.SingleOrDefaultAsync(x => x.CategoryName == categoryName);
        }

        public async Task<IList<Category>> GetSelectedCategories(IList<int> ids)
        {
            return  await _imsContext.Categories.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}