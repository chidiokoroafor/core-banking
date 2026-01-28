using CoreBanking.Data;
using CoreBanking.DTOs;
using CoreBanking.Entites;
using CoreBanking.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace CoreBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly BankContext _bankContext;
        private readonly PasswordHasher<Customer> _hasher = new PasswordHasher<Customer>();
        public AuthController(JwtHelper jwtHelper, BankContext bankContext)
        {
            _jwtHelper = jwtHelper;
            _bankContext = bankContext;
        }

        [HttpPost("registeration")]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

           
            var userExists = await _bankContext.Customers.AnyAsync(c=> c.Email == userForRegistration.Email);
            if (!userExists)
            {
                var user = new Customer
                {
                    FirstName = userForRegistration.FirstName,
                    LastName = userForRegistration.LastName,
                    Email = userForRegistration.Email,
                    DateOfBirth = userForRegistration.DateOfBirth,
                    CreatedAt = DateTime.UtcNow,
                    PhoneNumber = userForRegistration.PhoneNumber,
                };

                var hashedPassword = _hasher.HashPassword(user, userForRegistration.Password);
                user.PasswordHash = hashedPassword;
                await _bankContext.AddAsync(user);

                await _bankContext.SaveChangesAsync();
                return Created("", new {success=true, newUser = user});
            }

            return BadRequest(new { success = false, message= "Email entered already exists" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _bankContext.Customers.Where(c=>c.Email == loginDto.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password) == PasswordVerificationResult.Success;
                Console.WriteLine(result);
                if (!result )
                {
                    return Unauthorized(new { success = false, message = "Wrong Email or password" });

                }
            }

            var token = _jwtHelper.GenerateToken(user);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }
}
