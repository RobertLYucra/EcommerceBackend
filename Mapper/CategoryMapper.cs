using EcommerceBackend.Contracts;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;

namespace EcommerceBackend.Mapper
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryResponse  CategoryResponseF(Category category)
        {
            return new CategoryResponse
            {
                CategoryId  = category.CategoryId,
                CategoryName = category.CategoryName,
                Imagen = category.Imagen,
                CategoryDescription = category.CategoryDescription,
                Created = category.Created,
            };
        }
    }
}
