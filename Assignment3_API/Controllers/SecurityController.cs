//Inspired by LP10 Security Sourse Code from clickup (CourseController.cs)
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment3_Backend.Models;
using Assignment3_Backend.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment3_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public SecurityController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //LP10 ClickUP
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserViewModel uvm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(uvm.emailaddress);
            if (user != null)
            {
                return Conflict("Account already exists.");
            }

            var newUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = uvm.emailaddress,
                Email = uvm.emailaddress
            };

            var result = await _userManager.CreateAsync(newUser, uvm.password);
            if (!result.Succeeded)
            {
                // Log the specific error details or return a more meaningful error message
                // You can access the errors via result.Errors property
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
            }

            return Ok();
        }


        //Ai Generated
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserViewModel uvm)
        {
            var user = await _userManager.FindByNameAsync(uvm.emailaddress);

            if (user != null && await _userManager.CheckPasswordAsync(user, uvm.password))
            {
                try
                {
                    var token = GenerateJWTToken(user);
                    return Created("", new { token, user = user.UserName });
                }
                catch (Exception ex)
                {
                    // Log the specific exception details for troubleshooting
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while generating the JWT token.");
                }
            }

            return Unauthorized("Invalid credentials");
        }


        //Ai Generated
        [HttpGet]
        private string GenerateJWTToken(AppUser user)
        {
            var claims = new List<Claim>
         {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
          };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = credentials,
                Issuer = _configuration["Tokens:Issuer"],
                Audience = _configuration["Tokens:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        //Ai Generated
        [HttpPost]
        [Route("Logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }
    }
}
