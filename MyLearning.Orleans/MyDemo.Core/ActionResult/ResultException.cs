using System;
using System.Collections.Generic;
using System.Text;

namespace MyDemo.Core
{
    public class ResultException : Exception
    {
        public ResultException(string message) : base(message)
        {
        }

        public ResultException(string message, Exception e) : base(message, e)
        {

        }
    }
}
