using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using PokemonDatabaseAPI.Model.Dto;

namespace PokemonDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IPokemonDbContext pokemonDbContext, IAuthService authService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            try
            {
                var user = new User
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PasswordHashed = null,
                    Role = request.Role
                };
                user.PasswordHashed = new PasswordHasher<User>().HashPassword(user, request.Password);
                await pokemonDbContext.Users.AddAsync(user);
                await pokemonDbContext.SaveChangesAsync();
                return Ok("Registration Successful");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            try
            {
                var user = await pokemonDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null) return Unauthorized("Invalid Username or Password");

                var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHashed, request.Password);

                if (result == PasswordVerificationResult.Failed) return Unauthorized("Invalid Username or Password");

                var token = authService.CreateToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

}
