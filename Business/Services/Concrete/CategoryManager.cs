using AutoMapper;
using Business.Services.Abstract;
using Business.Utilities.Exceptions;
using Core.Utilities.Exceptions;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        public CategoryManager(/*ICategoryRepository categoryRepository,*/ IMapper mapper,IUnitOfWork unitOfWork)
        {
            //_categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<List<GetAllCategoryDto>>(categories);
        }

        public async Task<GetCategoryDto> GetCategoryById(Guid id)
        {
            var result = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);
            if (result is null)
            {
                throw new NotFoundException(ExceptionMessage.CategoryNotFound);
            }
            return _mapper.Map<GetCategoryDto>(result);
        }
        public async Task AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            category.CreatedAt= DateTime.UtcNow;
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var result = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);
            if (result is null)
            {
                throw new NotFoundException(ExceptionMessage.CategoryNotFound);
            }
            _unitOfWork.CategoryRepository.Delete(result);
            await _unitOfWork.SaveAsync();
        }




    }
}
