using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("status")]
    public class StatusController : ControllerBase
    {
        public readonly IMachineStateService _MachineStateService;
        
        public StatusController(IMachineStateService machineStateService) => _MachineStateService = machineStateService;

        [HttpGet]
        [Route("valid/{idState}")]
        public ActionResult GetValidStatesAsync(int idState)
        {
            try
            {
                var validStates = _MachineStateService.ObtenerTransicionesValidas((TaskStates)idState);
                return !validStates.IsNullOrEmpty() ? StatusCode(200, validStates) : StatusCode(409);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
