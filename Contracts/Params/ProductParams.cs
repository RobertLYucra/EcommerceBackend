namespace EcommerceBackend.Contracts.Params
{
    public class ProductParams
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string? Imagen { get; set; }
        public string Categoria { get; set; }
        public string? Description { get; set; }
        public bool? IsDelete { get; set; }

        public void Validate()
        {
            if(string.IsNullOrEmpty(ProductName))
            {
                throw new Exception("El nombre de producto no debe ser nulo o vacío") ;
            }
            if(string.IsNullOrEmpty(Categoria))
            {
                throw new Exception("La categoría no debe ser nulo o vacío");
            }
            if(Price <=0)
            {
                throw new Exception("El precio debe ser mayor que 0");
            }
        }
    }
}
