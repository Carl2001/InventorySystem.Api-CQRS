using InventorySystem.Api.Data;
using InventorySystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace InventorySystem.Api.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly ApplicationDbContext _context;
        public ProductQueries(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}