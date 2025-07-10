using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Application.Contracts.Infrastructure.identity;
using ProjectManagementApp.Application.Models.Auth;
using System.Threading.Tasks;

namespace ProjectManagementApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenRequestModel model)
        {
            var result = await _authService.TokenGeneratorAsync(model);
            if (!result.IsAuthenticated)
                return Unauthorized(result);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ResgisterUserModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result.IsSuccessful)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignmentModel model)
        {
            var result = await _authService.RoleAssignment(model);
            if (result == "User not found.")
                return NotFound(result);
            if (result != "Role assigned successfully.")
                return BadRequest(result);
            return Ok(result);
        }
    }
}
