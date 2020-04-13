using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDemo.Core;

namespace MyDemo.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        // GET: api/Result
        [HttpGet]
        public Result<IEnumerable<string>> Get()
        {
            return Result<IEnumerable<string>>.Ok(new string[] { "value1", "value2" });
        }

        // GET: api/Result/5
        [HttpGet("{id}", Name = "Get")]
        public Result<string> Get(int id)
        {
            return Result<string>.Ok("value");
        }

        // POST: api/Result
        [HttpPost]
        public Result<string> Post([FromBody] string value)
        {
            return Result<string>.Ok(value);
        }

        // PUT: api/Result/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
