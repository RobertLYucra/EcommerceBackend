using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Domain;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductResource _productResource;

        public ProductController(IProductResource productResource)
        {
            _productResource = productResource;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _productResource.GetAllProducts();
            return Ok(new MessageResponseList<ProductResponse>(true, "Todos los produsctos", allProducts));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            try
            {
                var product = await _productResource.GetProductById(productId);
                if (product != null) return Ok(new MessageResponse(true, "Producto encontrado", product));

                return NotFound(new MessageResponse(false, "Producto no encontrado", null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductParams productParams)
        {
            try
            {
                var product = await _productResource.CreateProduct(productParams);
                if (product != null) return Created("Created", new MessageResponse(true, "Producto creado", product));

                return BadRequest(new MessageResponse(false, "No se puedo crear el producto", null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(ProductParams productParams, Guid productId)
        {
            try
            {
                var product = await _productResource.UpdateProduct(productParams, productId);
                if (product != null) return Ok(new MessageResponse(true, "Producto actualizado", product));

                return BadRequest(new MessageResponse(false,"Error al acatualizar",null));
            }
            catch (Exception ex)
            {
                return BadRequest(new MessageResponse(false, "Error al acatualizar", ex));
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                var product =  await  _productResource.DeleteProduct(productId);
                if (product != null) return Ok(new MessageResponse(true, "Producto eliminado", product));
                return NotFound(new MessageResponse(false,"Error al eliminar",null));
            }catch(Exception ex)
            {
                return NotFound(new MessageResponse(false, "Error al eliminar", null));
            }
        }
    }
}
