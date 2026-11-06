using InventorySystem.Api.Commands;
using InventorySystem.Api.Models;
using InventorySystem.Api.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryQueries _queries;
        private readonly CategoryCommands _commands;
        public CategoryController(CategoryQueries queries, CategoryCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _queries.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            await _commands.CreateAsync(category);
            return Ok();
        }
    }
}