using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerseApp.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }
        [HttpGet]
        public async Task<ActionResult<ShoppingCart?>> GetCartAsync(string id)
        {
            var cart = await cartService.GetCartAsync(id);


            return Ok(cart);
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart?>> SetCartAsync(ShoppingCart cart)
        {
            var updatedCart = await cartService.SetCartAsync(cart);
            if (updatedCart == null)
            {
                return BadRequest("Problem with cart");
            }
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCartAsync(string key)
        {
            var result = await cartService.DeleteCartAsync(key);
            if (result == null)
            {
                return BadRequest("Problem is delete Key");
            }
            return Ok();
        }
    }
}
