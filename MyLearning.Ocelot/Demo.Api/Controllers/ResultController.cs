using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        [HttpGet]
        public Result<Store> GetResult()
        {
            throw new ResultException("asdasd");
            return Result.Ok(new Store());
        }
    }
}