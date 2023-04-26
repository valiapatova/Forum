﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Exceptions
{
    public class AuthenticationException : ApplicationException
    {
        public AuthenticationException()
        {
        }
        public AuthenticationException(string message):base(message)
        {

        }
    }
}
