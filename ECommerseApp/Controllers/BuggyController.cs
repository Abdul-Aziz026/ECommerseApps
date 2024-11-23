using Core.Entities;
using ECommerseApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerseApp.Controllers
{
    [ApiController]
    [Route("Buggy/")]
    public class BuggyController : Controller
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is a test exception...");
        }


        [HttpPost("validationerror")]
        public IActionResult GetValidationError(CreateProductDto product)
        {
            Console.WriteLine("Dto Product: " + product.Id);
            if (product == null)
            {
                throw new NullReferenceException();
            }
            return Ok();
        }
    }
}
