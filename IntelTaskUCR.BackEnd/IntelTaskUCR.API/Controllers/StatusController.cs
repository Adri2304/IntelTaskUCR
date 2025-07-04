using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("states")]
    public class StatusController : ControllerBase
    {
        public readonly IMachineStateService _MachineStateService;
        public readonly IStatusService _statusService;

        public StatusController(IMachineStateService machineStateService, IStatusService statusService) {
            _MachineStateService = machineStateService;
            _statusService = statusService;
        }

        [HttpGet]
        [Route("valid/{idState}")]
        public ActionResult GetValidStatesAsync(int idState)
        {
            try
            {
                var validStates = _MachineStateService.ObtenerTransicionesValidas((TaskStates)idState);
                return StatusCode(200, validStates);
                //return !validStates.IsNullOrEmpty() ? StatusCode(200, validStates) : StatusCode(409);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("states")]
        public async Task<ActionResult> ReadStatesAsync()
        {
            try
            {
                var response = await _statusService.ReadStatesAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("complexities")]
        public async Task<ActionResult> ReadComplexitiesAsync()
        {
            try
            {
                var response = await _statusService.ReadComplexitiesAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("priorities")]
        public async Task<ActionResult> ReadPrioritiesAsync()
        {
            try
            {
                var response = await _statusService.ReadPrioritiesAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
