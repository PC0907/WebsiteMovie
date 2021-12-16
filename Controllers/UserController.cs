using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using DotNetAssignment.Models;

namespace DotNetAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getData")]
        public IEnumerable<User> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new User
            {
                username = "test",
                hashpassword = "test",
                mail = "test"
            })
            .ToArray();
        }

       [Authorize]
       [HttpGet("checkAuth")]
       public bool CheckAuthentication()
        {
            Console.WriteLine(HttpContext.Request.Cookies.ContainsKey("AuthCookie"));
            if (HttpContext.Request.Cookies.ContainsKey("AuthCookie")) return true;
            return false;

        }


       [HttpGet("test")]
       public bool Check()
        {
            Console.WriteLine("heyy");
            return true;

        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] User user)
        {
            /*
            if (UserServices.LoginUser(user))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.mail)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return StatusCode(202);
            }
            */
            return StatusCode(401);
            
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return Redirect("login");
        }
        /*
        [HttpPost("signup")]   //Route to register the user


        public IActionResult RegisterUser([FromForm] User user) 
        {
            if (UserServices.RegisterUser(user))
            {
               return StatusCode(201);
            }
           return StatusCode(406);
           
        }
        */
        /*        
        [HttpGet("getUser")]

        public async Task<User>GetUser([FromQuery] string username)
        {

            return await UserServices.GetUser(username);
        }
        */
    }
}
