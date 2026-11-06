using InventorySystem.Api.Data;
using InventorySystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InventorySystem.Api.Queries
{
    public class InventoryMovementQueries
    {
        private readonly ApplicationDbContext _context;
        public InventoryMovementQueries(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<InventoryMovement>> GetByProductAsync(int productId)
        {
            return await _context.InventoryMovements.AsNoTracking().Where(m => m.ProductId == productId).ToListAsync();
        }
    }
}