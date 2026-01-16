using Core.DataAccess.Repositories.Concrete.EFCore;
using DataAccess.EFCore;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.Concrete
{
    public class EFCategoryRepository:EFBaseRepository<Category,AppDbContext>,ICategoryRepository
    {
        public EFCategoryRepository(AppDbContext context):base(context)
        {
        }
    }
}
