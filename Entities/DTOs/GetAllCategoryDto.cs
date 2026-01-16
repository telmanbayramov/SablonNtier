using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class GetAllCategoryDto:IDto
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
