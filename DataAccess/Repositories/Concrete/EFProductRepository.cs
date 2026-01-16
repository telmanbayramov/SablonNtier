using Core.DataAccess.Repositories.Concrete.EFCore;
using DataAccess.EFCore;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.Concrete
{
    public class EFProductRepository:EFBaseRepository<Product,AppDbContext>,IProductRepository
    {
        public EFProductRepository(AppDbContext context):base(context)
        {
        }
    }
}
