using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Domain;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginResource _loginResource;
        public LoginController(ILoginResource loginResource)
        {
            _loginResource = loginResource;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var user = _loginResource.GetUserLogin(userLogin);
                if(user == null)
                {
                    return NotFound(new MessageResponseObject(false,"Credenciales incorrectas",null));
                }
                return Ok(new MessageResponseObject(true, "Inicio de sesión exitoso...", user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
