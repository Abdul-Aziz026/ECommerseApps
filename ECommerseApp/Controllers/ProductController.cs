using Core.Interface;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerse_App.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductController : ControllerBase
    // public class ProductController (IGenericRepository<Product> _repo) : ControllerBase
    {
        private readonly IGenericRepository<Product> repo;
        private readonly IProductRepository repoo;

        public ProductController(IGenericRepository<Product> _repo, IProductRepository _repoo)
        {
            repo = _repo;
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
            /*var res = await repo.ListAllAsync();
            return Ok(res);*/
            var products = await repoo.GetProducts(brand, type, sort);
            return Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {

            var product = await repo.GetByIdAsync(id);
            if (product == null)
                return NotFound($"No product found with ID: {id}");

            return Ok(product);

            /*var product = await repo.GetProductById(id);
            return Product;*/
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            // implement code...
            // return Ok();

            var brands = await repoo.GetBrands();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            // implement code...
            // return Ok();

            var types = await repoo.GetTypes();
            return Ok(types);
        }

        [HttpPost("addproducts")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                await repo.Add(product);
                return Ok(new {message = "Product Add Successful!"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            /*var result = await repo.AddProduct(product);
            if (result == "Added Product...")
                return Ok(result);

            return BadRequest(result);*/
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            try
            {
                bool exist = await repo.Exist(id);
                Console.WriteLine("IsExist: " + exist);
                if (exist == false)
                {
                    return NotFound($"Not found with ID: {id}");
                }
                await repo.Update(product);
                return Ok("Update product successful!");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update product");
            }
            /*var exists = await repo.ExistProduct(id);
            if (!exists)
                return NotFound($"No product found with ID: {id}");

            var result = await repo.UpdateProduct(id, product);
            if (result == 1)
                return Ok("Product updated successfully.");

            return BadRequest("Failed to update the product.");*/
            
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await repo.Remove(id);
                return Ok("Remove Product Successful!");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete the product.");
            }

            /*var exists = await repo.ExistProduct(id);
            if (!exists)
                return NotFound($"No product found with ID: {id}");

            var result = await repo.DeleteProduct(id);
            if (result)
                return Ok("Product deleted successfully.");

            return BadRequest("Failed to delete the product.");*/
        }
    }
}
