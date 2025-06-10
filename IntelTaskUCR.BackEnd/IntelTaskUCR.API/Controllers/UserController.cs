using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.API.DTOs.Users;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        [HttpGet]
        [Route("read/{id?}")]
        public async Task<ActionResult> ReadUsersAsync(int? id)
        {
            try
            {
                var response = await _userService.ReadUsersAsync(id);
                return response.IsNullOrEmpty() ? StatusCode(204) : StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserDTO newUserData)
        {
            try
            {
                var newUser = new User(
                newUserData.IdUser,
                newUserData.UserName,
                newUserData.Mail,
                newUserData.BirthDate,
                newUserData.Password,
                newUserData.IdRole);

                var response = await _userService.CreateUserAsync(newUser);
                return response == true ? StatusCode(201) : StatusCode(409);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("update/{idUser}")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdatedUserDTO newData, int idUser)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(idUser, newData.GetAtrubutesDictionary());
                return response == true ? StatusCode(200, "Actualizado correctante.") : StatusCode(409);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{idUser}")]
        public async Task<ActionResult> DeleteUserAsync(int idUser)
        {
            try
            {
                var response = await _userService.DeleteUserAsync(idUser);
                return response == true ? StatusCode(200, "Eliminado correctamente.") : StatusCode(409);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
