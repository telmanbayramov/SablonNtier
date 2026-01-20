using AutoMapper;
using Business.Services.Abstract;
using Business.Utilities.Exceptions;
using Core.Entities.Abstract;
using Core.Utilities.Exceptions;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.CategoryDtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;


//DB->REP->SER->CON
namespace Business.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        //private readonly ICategoryRepository _categoryRepository;     -unit of worksuz
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryManager(/*ICategoryRepository categoryRepository,*/ IMapper mapper, IUnitOfWork unitOfWork)
        {
            //_categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<List<GetAllCategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            if (categories.Count == 0)
            {
                return new ErrorDataResult<List<GetAllCategoryDto>>(_mapper.Map<List<GetAllCategoryDto>>(categories), "mehsul yoxdu");
            }
            return new SuccessDataResult<List<GetAllCategoryDto>>(_mapper.Map<List<GetAllCategoryDto>>(categories), "mehsullar siyahilandi");
        }

        public async Task<IDataResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            var result = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);
            if (result is null)
            {
                return new ErrorDataResult<GetCategoryDto>(_mapper.Map<GetCategoryDto>(result), $"{id}-li mehsul tapilmadi");
            }
            return new SuccessDataResult<GetCategoryDto>(_mapper.Map<GetCategoryDto>(result), $"{id}-li mehsul tapildi");
        }
        public async Task<IResult> AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {


            var category = _mapper.Map<Category>(createCategoryDto);
            category.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.CategoryRepository.AddAsync(category);
            var result = await _unitOfWork.SaveAsync();
            if (result == 0)
            {
                return new ErrorResult("elave edilmedi");
            }
            return new SuccessResult("elave olundu");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);
            if (category is null)
            {
                throw new NotFoundException(ExceptionMessage.CategoryNotFound);
            }
            _unitOfWork.CategoryRepository.Delete(category);
            var result = await _unitOfWork.SaveAsync();
            if (result == 0)
            {
                return new ErrorResult("mehsul silinmedi");
            }
            return new SuccessResult("mehsul silindi");
        }
    }
}
