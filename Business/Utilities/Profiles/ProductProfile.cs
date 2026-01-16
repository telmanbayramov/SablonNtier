using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductDto, Product>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
        }
    }
}
