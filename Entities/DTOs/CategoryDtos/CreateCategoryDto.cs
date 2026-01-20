using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs.CategoryDtos
{
    public class CreateCategoryDto : IDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
