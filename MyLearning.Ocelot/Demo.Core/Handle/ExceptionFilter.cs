using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Demo
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ExceptionFilter(
            ILogger<ExceptionFilter> logger,
            IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            //if (_hostEnvironment.IsDevelopment())
            //    return;

            if (context.Exception is ResultException == false)
                _logger.LogError(1, context.Exception, context.Exception.Message);

            context.Result = new JsonResult(Result<object>.FromCode(ResultCode.Fail, context.Exception.Message));

            context.HttpContext.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            context.ExceptionHandled = true;
        }

    }
}
