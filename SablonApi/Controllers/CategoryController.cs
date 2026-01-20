using Business.Services.Abstract;
using Entities.DTOs.CategoryDtos;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "User")]

        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryservice.GetAllCategoriesAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var result = await _categoryservice.GetCategoryById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
        {
            var result=await  _categoryservice.AddCategoryAsync(createCategoryDto);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
