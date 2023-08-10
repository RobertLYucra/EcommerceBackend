using EcommerceBackend.Contracts;
using EcommerceBackend.Domain;

namespace EcommerceBackend.Mapper.Abstract
{
    public interface ICategoryMapper
    {
        public CategoryResponse CategoryResponseF(Category category);
    }
}
