using InventorySystem.Api.Commands;
using InventorySystem.Api.Models;
using InventorySystem.Api.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQueries _queries;
        private readonly ProductCommands _commands;
        public ProductsController(IProductQueries queries, ProductCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _queries.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _queries.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _commands.CreateAsync(product);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            await _commands.UpdateAsync(product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commands.DeleteAsync(id);
            return Ok();
        }
    }
}