using AutoMapper;
using Business.Services.Abstract;
using DataAccess.UnitOfWork.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDtos;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Concrete
{
    public class ProductManager:IProductService
    {
        IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllProductDto>> GetAllProductAsync()
        {
            var products= await _unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<List<GetAllProductDto>>(products);
        }

        public async Task AddProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
