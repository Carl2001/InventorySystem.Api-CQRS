using InventorySystem.Api.Data;
using InventorySystem.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace InventorySystem.Api.Queries
{
    public class CategoryQueries
    {
        private readonly ApplicationDbContext _context;
        public CategoryQueries(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }
    }
}