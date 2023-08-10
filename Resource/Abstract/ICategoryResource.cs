using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Params;

namespace EcommerceBackend.Resource.Abstract
{
    public interface ICategoryResource
    {
        public Task<List<CategoryResponse>> GetAllCategories();
        public  Task<CategoryResponse> GetCategoryById(Guid categoryId);
        public Task<CategoryResponse> CreateCategory(CategoryParams categoryParams);
        public  Task<CategoryResponse> UpdateCategory(CategoryParams categoryParams, Guid categoryId);
        public  Task<bool> DeleteCategory(Guid categoryId);
    }
}
