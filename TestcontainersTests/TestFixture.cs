using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MongoDb;
using TestcontainersApi.Models;

namespace TestcontainersTests
{
    public class TestFixture : WebApplicationFactory<Program>
    {
        private readonly MongoDbContainer _container;

        public TestFixture()
        {
            _container = new MongoDbBuilder()
                .WithImage("mongo:4.4.26")
                .WithUsername("")
                .WithPassword("")
                .Build();

            _container.StartAsync().Wait();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    services.PostConfigure<MongoDbOptions>(settings =>
                    {
                        settings.Uri = _container.GetConnectionString();
                    });
                });

            return base.CreateHost(builder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //_container?.DisposeAsync().AsTask().Wait();
        }
    }
}
