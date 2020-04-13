using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyDemo.Grains;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace MyDemo.SiloHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseOrleans(builder =>
                {
                    builder
                        .UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "HelloWorldApp";
                        })
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                        .AddMemoryGrainStorage(name: "ArchiveStorage")
                        .AddMemoryGrainStorageAsDefault()
                        .ConfigureLogging(logging => logging.AddConsole())

                        //.AddAzureBlobGrainStorage(
                        //    name: "profileStore",
                        //    configureOptions: options =>
                        //    {
                        //        // Use JSON for serializing the state in storage
                        //        options.UseJson = true;

                        //        // Configure the storage connection key
                        //        options.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1";
                        //    })
                        ;
                })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                });

        //public static Task Main(string[] args)
        //{
        //    return new HostBuilder()
        //        .UseOrleans(builder =>
        //        {
        //            builder
        //                .UseLocalhostClustering()
        //                .Configure<ClusterOptions>(options =>
        //                {
        //                    options.ClusterId = "dev";
        //                    options.ServiceId = "HelloWorldApp";
        //                })
        //                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
        //                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
        //                .AddMemoryGrainStorage(name: "ArchiveStorage")
        //                //.AddAzureBlobGrainStorage(
        //                //    name: "profileStore",
        //                //    configureOptions: options =>
        //                //    {
        //                //        // Use JSON for serializing the state in storage
        //                //        options.UseJson = true;

        //                //        // Configure the storage connection key
        //                //        options.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=data1;AccountKey=SOMETHING1";
        //                //    })
        //                ;
        //        })
        //        .ConfigureServices(services =>
        //        {
        //            services.Configure<ConsoleLifetimeOptions>(options =>
        //            {
        //                options.SuppressStatusMessages = true;
        //            });
        //        })
        //        .ConfigureLogging(builder =>
        //        {
        //            builder.AddConsole();
        //        })
        //        .RunConsoleAsync();
        //}
    }
}
