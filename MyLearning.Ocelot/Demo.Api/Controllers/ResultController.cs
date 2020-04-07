using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        [HttpGet]
        public Result<Store> GetResult()
        {
            //throw new ResultException("asdasd");

            return Result<Store>.Ok(new Store());
        }

        [HttpPost]
        public async Task<Result<object>> Post()
        {
            var stream = new StreamReader(Request.Body);
            var resStr = await stream.ReadToEndAsync();

            if (JsonConvert.DeserializeObject(resStr) is JObject jObject && jObject.ContainsKey("errors"))
            {
                throw new ResultException(resStr);
            }

            return Result<object>.Ok(resStr);
        }
    }
}