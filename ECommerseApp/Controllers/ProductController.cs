using Core.Interface;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Repository;

namespace ECommerse_App.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductController : ControllerBase
    // public class ProductController (IGenericRepository<Product> _repo) : ControllerBase
    {
        private readonly IProductRepository repoo;

        public ProductController(IProductRepository _repoo)
        {
            repoo = _repoo;
        }

        [HttpGet("")]
        public IActionResult Root()
        {
            return Ok("Root Page...");
        }

        [HttpGet("products")]
        public async Task<ActionResult<List<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            var products = await repoo.GetProducts(brand, type, sort);
            return Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await repoo.GetProductById(id);
            if (product == null)
                return NotFound($"No product found with ID: {id}");

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            return Ok(await repoo.GetBrands());
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            return Ok(await repoo.GetTypes());
        }

        [HttpPost("addproducts")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                await repoo.AddProduct(product);
                return Ok(new {message = "Product Add Successful!"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            try
            {
                bool exist = await repoo.ExistProduct(id);
                Console.WriteLine("IsExist: " + exist);
                if (exist == false)
                {
                    return NotFound($"Not found with ID: {id}");
                }
                await repoo.UpdateProduct(id, product);
                return Ok("Update product successful!");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update product");
            }
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await repoo.DeleteProduct(id);
                return Ok("Remove Product Successful!");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete the product.");
            }
        }
    }
}
