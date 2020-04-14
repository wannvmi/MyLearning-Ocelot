using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDemo.Core;

namespace MyDemo.Client
{
    public class BaseController : Controller
    {
        [NonAction]
        public virtual Result<object> Success(string message = null)
        {
            return Result<object>.FromCode(ResultCode.Ok, message, new object());
        }

        [NonAction]
        public virtual Result<T> Success<T>(T data, string message = null) where T : class
        {
            return Result<T>.FromCode(ResultCode.Ok, message, data);
        }

        [NonAction]
        protected virtual Result<object> Error(string message)
        {
            return Result<object>.FromError(message, new object());
        }

        [NonAction]
        protected virtual Result<T> Error<T>(T data, string message = null) where T : class
        {
            return Result<T>.FromError(message, data);
        }
    }
}
