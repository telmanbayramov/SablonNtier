using Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface IProductService
    {
        public Task<List<GetAllProductDto>> GetAllProductAsync();
        public Task AddProduct(CreateProductDto createProductDto);
    }
}
