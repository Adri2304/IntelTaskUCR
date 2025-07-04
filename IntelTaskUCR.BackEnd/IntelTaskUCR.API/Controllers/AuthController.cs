using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IntelTaskUCR.Domain.Interfaces.Services;
using IntelTaskUCR.API.DTOs.Auth;
using Microsoft.IdentityModel.Tokens;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult> authenticateUser([FromBody] AuthDTO data)
        {
            try
            {
                if (!await _authService.AuthenticateUser(data.UserEmail, data.Password))
                    return StatusCode(409);

                var response = await _authService.GetAuthenticateUserInfoAsync(data.UserEmail);

                return !response.IsNullOrEmpty() ? StatusCode(200, response) : StatusCode(409);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
