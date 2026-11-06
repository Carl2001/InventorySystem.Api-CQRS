using InventorySystem.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace InventorySystem.Api.Queries
{
    public interface IProductQueries
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}