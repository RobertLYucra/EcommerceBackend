using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;

namespace EcommerceBackend.Resource
{
    public class CategoryResource : ICategoryResource
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapper _categoryMapper;

        public CategoryResource(ICategoryRepository categoryRepository, ICategoryMapper categoryMapper)
        {
            _categoryRepository = categoryRepository;
            _categoryMapper = categoryMapper;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return categories.Select(x => _categoryMapper.CategoryResponseF(x)).ToList();
        }

        public async Task<CategoryResponse> GetCategoryById(Guid categoryId)
        {
            var category = await _categoryRepository.GetCategoryById(categoryId);
            if (category != null) return _categoryMapper.CategoryResponseF(category);
            return null;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryParams categoryParams)
        {
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = categoryParams.CategoryName,
                Imagen = categoryParams.Imagen,
                CategoryDescription = categoryParams.CategoryDescription,
                Created = DateTime.Now
            };

            var created = await _categoryRepository.CreateCategory(category);
            return created ? _categoryMapper.CategoryResponseF(category) : null;
        }

        public async Task<CategoryResponse> UpdateCategory(CategoryParams categoryParams, Guid categoryId)
        {
            var category = await  _categoryRepository.GetCategoryById(categoryId);

            if(category!=null)
            {
                category.CategoryName = categoryParams.CategoryName;
                category.CategoryDescription = categoryParams.CategoryDescription;
                category.Imagen = categoryParams.Imagen;

                var updated = await _categoryRepository.UpdateCategory(category, categoryId);
                return updated ? _categoryMapper.CategoryResponseF(category) : null;
            }
            return null;
        }
        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            var deleted = await _categoryRepository.DeleteCateogry(categoryId);
            return deleted;
        }
    }
}
