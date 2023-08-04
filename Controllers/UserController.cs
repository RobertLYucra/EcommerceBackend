using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaEncomienda.Contracts.Formats;
using SistemaEncomienda.Contracts.Response;

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
        public IActionResult Get()
        {
            try
            {
                var employees = _userResource.GetAllEmployees();
                return Ok(new MessageResponseList<UserResponse>(true, "Lista de empleados",employees));
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
                if(employe == null)
                {
                    return NotFound(new MessageResponseObject(false,"Empledo no encontado...",null));
                }
                return Ok(new MessageResponseObject(true, "Empledo encontrado :)", employe));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employe = _userResource.DeleteEmployee(id);
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
    }
}
