using Dapper;
using InventorySystem.Api.Models;
using Microsoft.Data.SqlClient;
namespace InventorySystem.Api.Commands
{
    public class CategoryCommands
    {
        private readonly string _connectionString;
        public CategoryCommands(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task CreateAsync(Category category)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
            await connection.ExecuteAsync(sql, category);
        }
    }
}