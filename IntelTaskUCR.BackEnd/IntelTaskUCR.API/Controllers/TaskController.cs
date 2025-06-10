using IntelTaskUCR.API.DTOs.Tasks;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("task")]
    public class TaskController : ControllerBase
    {
        
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService) => _taskService = taskService;

        [HttpGet]
        [Route("read/{idTask?}")]
        public async Task<ActionResult> ReadTaskAsync(int? idTask)
        {
            try
            {
                var response = await _taskService.ReadTaskAsync(idTask);
                return response.IsNullOrEmpty() ? StatusCode(204) : StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateTaskAsync([FromBody] TaskDTO data)
        {
            try
            {
                var newTask = data.GetAtributesDictionary();
                var response = await _taskService.CreateTaskAsync(newTask);
                return response ? StatusCode(201, "Se creo creo la tarea correctamente") : StatusCode(204);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{idTask}")]
        public async Task<ActionResult> UpdateTaskAsync(int idTask, [FromBody] TaskDTO data)
        {
            try
            {
                var newData = data.GetAtributesDictionary();
                var response = await _taskService.UpdateTaskAsync(idTask, newData);
                return response ? StatusCode(200, "Actualizado correctamente") : StatusCode(204);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpDelete]
        //[Route("delete/{idTask}")]
        //public async Task<ActionResult> DeleteTaskAsync(int idTask)
        //{
        //    try
        //    {
               
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
