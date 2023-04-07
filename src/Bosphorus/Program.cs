namespace Bosphorus
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                services
                    .AddOcelot() 
                    .AddDelegatingHandler<RequestInspector>();
            }).ConfigureAppConfiguration((host, config) =>
            {
                config.AddJsonFile("ocelot.json");
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>().Configure(async app => await app.UseOcelot());
            });
    }
    
    public class RequestInspector : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"\nDevam etmeden önce şu gelen Request içeriğini bir inceyelim\n{request.ToString()}\n");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
