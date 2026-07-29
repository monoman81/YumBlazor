using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                db.Categories.Remove(category);
                return await db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return new Category();
            }
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category _category)
        {
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == _category.Id);
            if (category != null)
            {
                category.Name = _category.Name;
                db.Categories.Update(category);
                await db.SaveChangesAsync();
                return category;
            }
            return _category;
        }
    }
}
