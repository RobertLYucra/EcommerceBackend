using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Domain;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginResource _loginResource;
        private readonly IUserResource  _userResource;
        public LoginController(ILoginResource loginResource, IUserResource userResource  )
        {
            _loginResource = loginResource;
            _userResource = userResource;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                userLogin.Validate();
                var user = _loginResource.GetUserLogin(userLogin);
                if(user == null)
                {
                    return NotFound(new MessageResponse(false,"Credenciales incorrectas",null));
                }
                return Ok(new MessageResponse(true, "Inicio de sesión exitoso...", user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/claims")]
        public IActionResult ValidateUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var validated = _loginResource.ValidateToken(identity);
            if (validated == null) return NotFound(new MessageResponse(false,"Token no valido",null));
            return NotFound(new MessageResponse(true, "Token validado", validated));
        }
    }
}
