using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Abstract
{
    public interface IDataResult<TEntity>
    {
        public TEntity Data { get; }
        public bool Success { get; }
        public string Message { get; }
    }
}
