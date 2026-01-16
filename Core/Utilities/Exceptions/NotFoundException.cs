using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message):base(message)
        {
            
        }
        public NotFoundException()
        {
            
        }
    }
}
