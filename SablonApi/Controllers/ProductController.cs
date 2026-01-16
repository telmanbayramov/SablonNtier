using Business.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SablonApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult>GetAllProduct()
        {
            return Ok( await _service.GetAllProductAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Entities.DTOs.ProductDtos.CreateProductDto createProductDto)
        {
            await _service.AddProduct(createProductDto);
            return Ok();
        }
    }
}
