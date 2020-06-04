namespace Demo.Api.Models
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}