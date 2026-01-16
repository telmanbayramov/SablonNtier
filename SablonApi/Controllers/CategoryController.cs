using Business.Services.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SablonApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly ICategoryService _categoryservice;

        public CategoryController(ICategoryService productService)
        {
            _categoryservice = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryservice.GetAllCategoriesAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            return Ok(await _categoryservice.GetCategoryById(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryservice.AddCategoryAsync(createCategoryDto);
            return Ok();
        }
    }
}
