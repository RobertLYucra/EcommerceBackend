using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Formats;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Domain;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryResource _categoryResource;

        public CategoryController(ICategoryResource categoryResource)
        {
            _categoryResource = categoryResource;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var AllCategories = await _categoryResource.GetAllCategories();
            return Ok(new MessageResponseList<CategoryResponse>(true, "Todas las categorías", AllCategories));
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetbyId(Guid categoryId)
        {
            var category = await _categoryResource.GetCategoryById(categoryId);
            if (category != null) return Ok(new MessageResponse(true, "Categoria encontrado", categoryId));
            return Ok(new MessageResponse(false, "Categoria no encontrado", null));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryParams categoryParams)
        {
            var  category = await _categoryResource.CreateCategory(categoryParams);
            if(category != null) return Created("Created",new MessageResponse(true, "Categoria creado", category));

            return BadRequest( new MessageResponse(false, "No se pudo crear la categoria", null));
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(CategoryParams categoryParams, Guid categoryId)
        {
            var category = await _categoryResource.UpdateCategory(categoryParams, categoryId);
            if (category != null) return Ok(new MessageResponse(false, "Categoría actualizado", category));

            return BadRequest(new MessageResponse(false, "No se pudo actualizar", null));
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var deleted = await _categoryResource.DeleteCategory(categoryId);

            return deleted ? Ok(new MessageResponse(true, "Categoría Eliminado", null)) 
                : BadRequest( new MessageResponse(false, "No se pude eliminar...", null));
        }
    }
}
