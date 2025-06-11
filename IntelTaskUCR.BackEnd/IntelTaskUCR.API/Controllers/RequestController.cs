using Microsoft.AspNetCore.Mvc;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using IntelTaskUCR.API.DTOs.Request;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("request")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService) => _requestService = requestService;

        [HttpGet]
        [Route("read/{idRequest?}")]
        public async Task<ActionResult> ReadRequestAsync(int? idRequest)
        {
            try
            {
                var response = await _requestService.ReadRequestAsync(idRequest);
                return response.IsNullOrEmpty() ? StatusCode(204) : StatusCode(200, response) ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create/")]
        public async Task<ActionResult> CreateRequestAsync(CreateRequestDTO newRequest)
        {
            try
            {
                var data = newRequest.GetAtributesDictionary();
                var response = await _requestService.CreateRequestAsync(data);
                return !response ? StatusCode(409) : StatusCode(201, "Se realizo correctamente") ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("user/{idUser}")]
        public async Task<ActionResult> ReadRequestPerUserAsync(int idUser)
        {
            var response = await _requestService.ReadRequestPerUserAsync(idUser);
            return response.IsNullOrEmpty() ? StatusCode(204) : StatusCode(200, response);
        }

        [HttpPatch]
        [Route("update/{idRequest}")]
        public async Task<ActionResult> ReadRequestPerUserAsync(UpdateRequestDTO newData, int idRequest)
        {
            var data = newData.GetAtributesDictionary();
            var response = await _requestService.UpdateRequestAsync(data, idRequest);
            return response ? StatusCode(200, "Se actualizo correctamente") : StatusCode(409);
        }

        //[HttpPatch]
        //[Route("status/{idRequest}")]
        //public async Task<ActionResult> ChangeRequestStatusAsync( int idRequest, [FromBody] int state,
        //    [FromBody] string? justification)
        //{

        //}
    }
}
