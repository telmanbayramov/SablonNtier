using Entities.DTOs.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Validations.Category
{
    public class CreateCategoryDtoValidator:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(p=>p.Name)
                .NotEmpty().WithMessage("Category name cannot be empty.")
                .MinimumLength(2).WithMessage("Category name must be at least 2 characters long.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Category description cannot be empty.")
                .MinimumLength(5).WithMessage("Category description must be at least 5 characters long.")
                .MaximumLength(500).WithMessage("Category description cannot exceed 500 characters.");

        }
    }
}
