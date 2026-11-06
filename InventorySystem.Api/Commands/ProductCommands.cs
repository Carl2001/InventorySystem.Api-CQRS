using Dapper;
using InventorySystem.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
namespace InventorySystem.Api.Commands
{
    public class ProductCommands
    {
        private readonly string _connectionString;
        public ProductCommands(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task CreateAsync(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Products (Name, Description, Price, Stock) VALUES (@Name, @Description, @Price, @Stock)";
            await connection.ExecuteAsync(sql, product);
        }
        public async Task UpdateAsync(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, Stock = @Stock WHERE Id = @Id";
            await connection.ExecuteAsync(sql, product);
        }
        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Products WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}