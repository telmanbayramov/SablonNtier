using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public Task <int> SaveAsync();
    }
}
