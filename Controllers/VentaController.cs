using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaResource _ventaResource;

        public VentaController(IVentaResource ventaResource)
        {
            _ventaResource = ventaResource;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllventas()
        {
            var ventaResponse = await _ventaResource.GetAllVentas();
            return Ok(new MessageResponse(true, "All ventas", ventaResponse));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenta(VentaParams ventaParams)
        {
            try
            {
                ventaParams.Validate();
                var ventaResponse = await _ventaResource.CreateVenta(ventaParams);
                if(ventaResponse != null ) return Created("Creates",new MessageResponse(true, "Created", ventaResponse));
                return BadRequest(new MessageResponse(false, "Error al crear", null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
