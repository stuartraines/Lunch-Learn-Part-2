using System.Threading.Tasks;
using Demo.Api.Models;
using Demo.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await this.productRepository.ListAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var productId = await this.productRepository.CreateAsync(request.Name, request.Price, request.Active);

            if (!productId.HasValue)
            {
                return new StatusCodeResult(500);
            }

            var product = await this.productRepository.GetAsync(productId.Value);

            return Ok(product);
        }
    }
}