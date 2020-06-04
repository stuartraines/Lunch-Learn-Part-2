namespace Demo.Api.Models
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}