using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public interface IResult
    {
        ResultCode code { get; set; }

        string message { get; set; }
    }

    public interface IResult<TType> : IResult
    {
        TType data { get; set; }
    }
}
