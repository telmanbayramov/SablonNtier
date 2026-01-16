using Core.DataAccess.Repositores.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository:IBaseRepository<Product>
    {
    }
}
