using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudStates.API.Dtos;
using CloudStates.API.Extensions;
using CloudStates.API.Services;

namespace CloudStates.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest request)
        {
            return Ok(await _authService.RegisterAsync(request));
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            return Ok(await _authService.LoginAsync(request));
        }

        [HttpGet("refresh")]
        [Authorize(AuthenticationSchemes = "Refresh")]
        [ProducesResponseType(typeof(RefreshResponse), StatusCodes.Status200OK)]
        public ActionResult<RefreshResponse> Refresh()
        {
            return Ok(_authService.Refresh(User.GetUserId()));
        }
    }
}