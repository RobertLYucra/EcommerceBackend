using EcommerceBackend.Domain;

namespace EcommerceBackend.Repository.Abstract
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(Guid categoryId);
        public Task<bool> CreateCategory(Category product);
        public Task<bool> UpdateCategory(Category product, Guid productId);
        public Task<bool> DeleteCateogry(Guid productId);
    }
}
