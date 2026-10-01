using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class ShoppingCartRepository(ApplicationDbContext db) : IShoppingCartRepository
    {

        private readonly ApplicationDbContext db = db;

        public async Task<bool> ClearCartAsync(string? userId)
        {
            var cartItems = await db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();
            db.ShoppingCart.RemoveRange(cartItems);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await db.ShoppingCart.Where(u => u.UserId == userId).Include(u => u.Product).ToListAsync();
        }

        public async Task<bool> UpdateCartAsync(string? userId, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            var cart = await db.ShoppingCart.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };
                await db.ShoppingCart.AddAsync(cart);
            }
            else
            {
                cart.Count += updateBy;
                if (cart.Count <= 0)
                    db.ShoppingCart.Remove(cart);
            }
            return await db.SaveChangesAsync() > 0;
        }
    }
}
