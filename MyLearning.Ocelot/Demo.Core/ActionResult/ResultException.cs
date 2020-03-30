using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
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
