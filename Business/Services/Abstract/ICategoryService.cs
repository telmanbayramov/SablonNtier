using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface ICategoryService
    {
        public Task<List<GetAllCategoryDto>> GetAllCategoriesAsync();
        public Task<GetCategoryDto> GetCategoryById(Guid id);
        public Task AddCategoryAsync(CreateCategoryDto createCategoryDto);
        public Task Delete(Guid id);
    }
}
