using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserResource _userResource;
        public UserController(IUserResource userResource)
        {
            _userResource = userResource;
          
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {              
                var employees = await _userResource.GetAllEmployees();
                return Ok(new MessageResponseList<UserResponse>(true, "Lista de empleados", employees));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles ="Administrador")]
        [HttpGet]
        [Route("/autorize")]
        public async Task<IActionResult> GetAutoRize()
        {
            try
            {
                var employees = await _userResource.GetAllEmployees();
                return Ok(new MessageResponseList<UserResponse>(true, "Lista de empleados", employees));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var employe = await _userResource.GetById(id);
                if (employe != null)
                {
                    return Ok(new MessageResponse(true, "Empledo encontrado :)", employe));
                }
                return NotFound(new MessageResponse(false, "Empledo no encontado...", null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            try
            {
                var employe = await _userResource.DeleteEmployee(userId);
                if (!employe)
                {
                    return NotFound(new MessageResponse(false, "El empleado no fue encontrado o ya está desactivado.", null));
                }
                return Ok(new MessageResponse(true, "Empleado eliminado correctamente.", null));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar al empleado.", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(UserParams userParams)
        {
            userParams.Credentials!.Validate();
            var userResponse = await _userResource.CreateEmployee(userParams);
            if(userResponse != null)
            {
                return Ok(new MessageResponse(true,"Usuario creado",userResponse));
            }
            return BadRequest(new MessageResponse(false,"Error al crear usuario",null));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(UserParams userParams, Guid userId)
        {
            try
            {
                var employee = await _userResource.UpdateEmployee(userParams, userId);
                if (employee != null) return Ok(new MessageResponse(true, "User updated...", employee));
                return BadRequest(new MessageResponse(true, "User updated...", null));
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponse(false, ex.Message, null));
            }

        }
    }
}
