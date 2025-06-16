using Core.Entities.DTO;
using Core.Interface;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerseApp.Controllers
{
    [ApiController]
    [Route("api/account/")]
    public class UserController : Controller
    {

        private readonly IUserServices _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserServices userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [HttpPost("register")]
        [AllowAnonymous]  // Allow anonymous access to registration
        public async Task<IActionResult> RegisterUser(RegisterDTO user)
        {
            var registerUser = await _userService.RegisterUser(user.Username, user.Password, user.Role);

            var userDto = new UserDTO
            {
                Username = registerUser.Username,
                Token = _tokenService.CreateToken(registerUser)
            };

            return Ok(userDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]  // Allow anonymous access to login
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            Console.WriteLine("login user username: " + loginDto.Username + " password: " + loginDto.Password);
            try
            {
                var loginUser = await _userService.Login(loginDto.Username, loginDto.Password);
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


        [HttpGet("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult AdminAuthorizationTest()
        {
            return Ok("Admin Api Accessed...");
        }

        [HttpGet("user")]
        [Authorize(Roles="user")]
        public ActionResult UserAuthorizationTest()
        {
            return Ok("User Api Accessed...");
        }

        [HttpPost("logout")]
        // [Authorize]
        [AllowAnonymous]  // Allow anonymous access to registration
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Assuming you store blacklisted tokens in Redis
            // await _redis.SetAsync($"blacklistedToken:{token}", true, TimeSpan.FromHours(1));

            return Ok(new { message = "Logged out successfully." });
        }
    }
}
