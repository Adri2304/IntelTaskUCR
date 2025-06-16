using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IntelTaskUCR.Domain.Interfaces.Services;
using IntelTaskUCR.API.DTOs;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult> AutenticateUser([FromBody] AuthDTO data)
        {
            try
            {
                var response = await _authService.AutenticateUser(data.UserEmail, data.Password);
                return response ? StatusCode(200, "Autenticado correctamente") : StatusCode(409);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
