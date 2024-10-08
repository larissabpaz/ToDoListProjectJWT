using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoListProjectJWT.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ToDoListProjectJWT.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private static List<Register> users = new List<Register>();
        private readonly IPasswordHasher<Register> _passwordHasher;

        public AuthController(IPasswordHasher<Register> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

    [HttpPost("register")]
        public IActionResult Register(Register user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            users.Add(user);
            return Ok(new { message = "Usuário registrado com sucesso" });
        }


    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        if (userLogin.Username == "usuario" && userLogin.Password == "senha")
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userLogin.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        return Unauthorized();
    }

    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok(new { message = "Você acessou um endpoint protegido!" });
    }
}

public class UserLogin
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}