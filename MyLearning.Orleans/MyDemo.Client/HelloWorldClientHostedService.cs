﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MyDemo.Interfaces;
using Orleans;
using Orleans.Runtime;

namespace MyDemo.Client
{
    public class HelloWorldClientHostedService : IHostedService
    {
        private readonly IClusterClient _client;

        public HelloWorldClientHostedService(IClusterClient client)
        {
            this._client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // example of calling grains from the initialized client
            var friend = this._client.GetGrain<IHello>(0);
            var response = await friend.SayHello("Good morning, my friend!");
            Console.WriteLine($"{response}");

            // example of calling IHelloArchive grqain that implements persistence
            var g = this._client.GetGrain<IHelloArchive>(0);
            response = await g.SayHello("Good day!");
            Console.WriteLine($"{response}");

            response = await g.SayHello("Good evening!");
            Console.WriteLine($"{response}");

            var greetings = await g.GetGreetings();
            Console.WriteLine($"\nArchived greetings: {Utils.EnumerableToString(greetings)}");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
