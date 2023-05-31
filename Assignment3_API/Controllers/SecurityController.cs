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
        public SecurityController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
