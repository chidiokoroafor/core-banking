using CoreBanking.Contracts;
using CoreBanking.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreBanking.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreationRequest request)
        {
            var loggedInId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customerId = Guid.Parse(loggedInId);

            var result = await _accountService.CreateAccount(request, customerId);

            if (result.success)
            {
                return Created("", result);
            }

            return BadRequest(result);

        }

        [HttpGet("{accountNumber}/statement")]
        public async Task<IActionResult> GetStatement(string accountNumber, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var loggedInId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customerId = Guid.Parse(loggedInId);

            var statement = await _accountService.GetStatementAsync(accountNumber, from, to, customerId);
            if (statement.success) 
                return Ok(statement);

            return BadRequest(statement);
        }
    }
}
