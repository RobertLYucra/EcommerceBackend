namespace EcommerceBackend.Contracts
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Imagen { get; set; }
        public string ?CategoryDescription { get; set; }
        public DateTime Created { get; set; }
    }
}
