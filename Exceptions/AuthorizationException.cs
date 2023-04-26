using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Exceptions
{
    public class AuthorizationException : ApplicationException
    {
        public AuthorizationException(string message) : base(message)
        {
        }
    }
}
