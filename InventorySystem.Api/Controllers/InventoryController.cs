using InventorySystem.Api.Commands;
using InventorySystem.Api.Models;
using InventorySystem.Api.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryMovementQueries _queries;
        private readonly InventoryMovementCommands _commands;
        public InventoryController(InventoryMovementQueries queries, InventoryMovementCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            return Ok(await _queries.GetByProductAsync(productId));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InventoryMovement movement)
        {
            movement.MovementDate = DateTime.UtcNow;
            await _commands.RegisterMovementAsync(movement);
            return Ok();
        }
    }
}