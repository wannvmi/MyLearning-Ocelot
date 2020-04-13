using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDemo.Core;
using MyDemo.Core.Entities;
using MyDemo.Interfaces;
using Orleans;

namespace MyDemo.Client.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HelloWorldController : Controller
    {
        private readonly IClusterClient _clusterClient;

        public HelloWorldController(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        [HttpGet]
        public async Task<string> SayHello(string greeting)
        {
            var friend = _clusterClient.GetGrain<IHello>(0);

            var response = await friend.SayHello("Good morning, my friend!");
            return response;
        }

        [HttpGet]
        public Result<Store> Get()
        {
            return Result<Store>.Ok(new Store());
        }
    }
}