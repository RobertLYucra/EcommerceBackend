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
        [Route("getAllUser")]
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
    }
}
