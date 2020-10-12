using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyCoreApi.Models;

namespace MyCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // POST: api/Login
        [HttpPost]
        public IActionResult Index([FromBody] LoginViewModel model)
        {
            if (model.Username=="admin" && model.Password == "123456")
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("{069BD1DF-72D8-474B-8950-2C3EB03B2D03}"));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    "Issuer",//发行者
                    "Audience",//受众
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, "admin"),
                        new Claim(ClaimTypes.Gender,"Male"),
                        new Claim(ClaimTypes.Name,model.Username),
                    },
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                );

                return Ok(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }
    }
}
