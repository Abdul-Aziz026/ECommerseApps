using Core.Entities.DTO;
using Core.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerseApp.Controllers
{
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserServices _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserServices userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [HttpPost("user/register")]
        [AllowAnonymous]  // Allow anonymous access to registration
        public async Task<IActionResult> RegisterUser(RegisterDTO user)
        {

            Console.WriteLine("username: " + user.Username + " password: " + user.Password + " role: " + user.Role);
            if (await UserExist(user.Username))
            {
                return BadRequest("User already Exist");
            }
            var registerUser = await _userService.RegisterUser(user);

            var userDto = new UserDTO
            {
                Username = registerUser.Username,
                Token = _tokenService.CreateToken(registerUser)
            };

            return Ok(userDto);
        }

        [HttpPost("user/login")]
        [AllowAnonymous]  // Allow anonymous access to registration
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            Console.WriteLine("login user username: " + loginDto.Username + " password: " + loginDto.Password);
            try
            {
                var loginUser = await _userService.Login(loginDto);
                var userDto = new UserDTO
                {
                    Username = loginDto.Username,
                    Token = _tokenService.CreateToken(loginUser)
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


        [HttpGet("user/admin")]
        [Authorize(Roles = "admin")]
        public ActionResult AdminAuthorizationTest()
        {
            return Ok("Admin Api Accessed...");
        }

        [HttpGet("user/user")]
        [Authorize]
        public ActionResult UserAuthorizationTest()
        {
            return Ok("User Api Accessed...");
        }

        [Authorize]
        [HttpPost("user/logout")]
        public IActionResult Logout()
        {
            // Inform the client to clear the token (e.g., by removing it from local storage or cookies)
            return Ok(new { message = "Logged out successfully." });
        }


        private async Task<bool> UserExist(string username)
        {
            bool haveUser = await _userService.UserExist(username);
            if (haveUser)
            {
                return true;
            }
            return false;
        }
    }
}
