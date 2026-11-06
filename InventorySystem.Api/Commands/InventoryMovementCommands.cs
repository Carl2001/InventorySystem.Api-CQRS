using Dapper;
using InventorySystem.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
namespace InventorySystem.Api.Commands
{
    public class InventoryMovementCommands
    {
        private readonly string _connectionString;
        public InventoryMovementCommands(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task RegisterMovementAsync(InventoryMovement movement)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var insertSql = "INSERT INTO InventoryMovements (ProductId, MovementType, Quantity, MovementDate) VALUES (@ProductId, @MovementType, @Quantity, @MovementDate)";
                await connection.ExecuteAsync(insertSql, movement, transaction);
                var updateSql = movement.MovementType == "ENTRY" ? "UPDATE Products SET Stock = Stock + @Quantity WHERE Id = @ProductId" : "UPDATE Products SET Stock = Stock - @Quantity WHERE Id = @ProductId";
                await connection.ExecuteAsync(updateSql, new { movement.Quantity, movement.ProductId }, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}