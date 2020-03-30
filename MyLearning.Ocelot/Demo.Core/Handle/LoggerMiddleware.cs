//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

//namespace Demo.Core.Handle
//{
//    public class LoggerMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public LoggerMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            context.Request.EnableBuffering();
//            var requestReader = new StreamReader(context.Request.Body);
//            byte[] result = new byte[context.Request.Body.Length];
//            context.Request.Body.Read(result, 0, (int)context.Request.Body.Length);

//            var requestContent = requestReader.ReadToEnd();
//            Console.WriteLine($"Request Body: {requestContent}");

//            await _next(context);

//            //context.Request.EnableBuffering();
//            //var requestReader = new StreamReader(context.Request.Body);

//            //var requestContent = requestReader.ReadToEnd();
//            //Console.WriteLine($"Request Body: {requestContent}");
//            //context.Request.Body.Position = 0;


//            //using (var ms = new MemoryStream())
//            //{
//            //    context.Response.Body = ms;
//            //    await _next(context);


//            //    ms.Position = 0;
//            //    var responseReader = new StreamReader(ms);

//            //    var responseContent = responseReader.ReadToEnd();
//            //    Console.WriteLine($"Response Body: {responseContent}");

//            //    ms.Position = 0;
//            //}
//        }
//    }
//}
