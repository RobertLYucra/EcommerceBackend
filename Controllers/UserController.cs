using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Helpers;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserResource _userResource;
        private readonly ILoginResource _loginResource;
        private readonly IConfiguration _configuration;
        public UserController(IUserResource userResource , ILoginResource loginResource , IConfiguration configuration)
        {
            _userResource = userResource;
           _loginResource = loginResource;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {              
                var employees = _userResource.GetAllEmployees();
                return Ok(new MessageResponseList<UserResponse>(true, "Lista de empleados", employees));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var employe = _userResource.GetById(id);
                if (employe == null)
                {
                    return NotFound(new MessageResponseObject(false, "Empledo no encontado...", null));
                }
                return Ok(new MessageResponseObject(true, "Empledo encontrado :)", employe));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            try
            {
                var employe = _userResource.DeleteEmployee(userId);
                if (!employe)
                {
                    return NotFound(new MessageResponseObject(false, "Error al eliminar Empleado", null));
                }
                return Ok(new MessageResponseObject(true, "Empledo eliminado...", null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(UserParams userParams, int userId)
        {
            try
            {
                var employee = await _userResource.UpdateEmployee(userParams, userId);
                if (employee != null) return Ok(new MessageResponseObject(true, "User updated...", employee));
                return BadRequest(new MessageResponseObject(true, "User updated...", null));
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponseObject(false, ex.Message, null));
            }

        }
    }
}
