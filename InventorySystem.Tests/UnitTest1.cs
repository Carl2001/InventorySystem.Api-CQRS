using InventorySystem.Api.Controllers;
using InventorySystem.Api.Models;
using InventorySystem.Api.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;
namespace InventorySystem.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var mockQueries = new Mock<IProductQueries>();
            mockQueries.Setup(q => q.GetByIdAsync(99)).ReturnsAsync((Product)null);
            var controller = new ProductsController(mockQueries.Object, null);
            var result = await controller.Get(99);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}