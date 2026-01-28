using CoreBanking.Contracts;
using CoreBanking.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreBanking.Controllers
{
    [Route("api/transfers")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransferController(ITransferService transferService, IHttpContextAccessor httpContextAccessor)
        {
            _transferService = transferService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] TransferRequestDto dto)
        {
            var loggedInId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customerId = Guid.Parse(loggedInId);
           
            var result = await _transferService.TransferAsync(dto, customerId);
            return Ok(result);
            
           
        }
    }
}
