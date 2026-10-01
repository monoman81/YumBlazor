using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product != null)
            {
                if (product.ImageUrl != null)
                {
                    var imagePath = Path.Combine(webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }
                db.Products.Remove(product);
                return await db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
            {
                return new Product();
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await db.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product _product)
        {
            var product = await db.Products.FirstOrDefaultAsync(c => c.Id == _product.Id);
            if (product != null)
            {
                product.Name = _product.Name;
                product.Price = _product.Price;
                product.Description = _product.Description;
                product.SpecialTag = _product.SpecialTag;
                product.CategoryId = _product.CategoryId;
                product.ImageUrl = _product.ImageUrl;
                db.Products.Update(product);
                await db.SaveChangesAsync();
                return product;
            }
            return _product;
        }
    }
}
