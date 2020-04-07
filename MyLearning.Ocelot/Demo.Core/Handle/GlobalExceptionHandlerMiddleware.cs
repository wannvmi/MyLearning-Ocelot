using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo.Core.Handle
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/json;charset=utf-8;";

                if (ex is ResultException == false)
                {
                    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger<GlobalExceptionHandlerMiddleware>();
                    logger.LogError(1, ex, ex.Message);
                }

                var json = Result<object>.FromCode(ResultCode.Fail, ex.Message);
                var error = JsonConvert.SerializeObject(json);

                await context.Response.WriteAsync(error);
            }
        }
    }
}
