using Core.Utilities.Result.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface ICategoryService
    {
        public Task<IDataResult<List<GetAllCategoryDto>>> GetAllCategoriesAsync();
        public Task<IDataResult<GetCategoryDto>> GetCategoryById(Guid id);
        public Task<IResult> AddCategoryAsync(CreateCategoryDto createCategoryDto);
        public Task<IResult> Delete(Guid id);
    }
}
