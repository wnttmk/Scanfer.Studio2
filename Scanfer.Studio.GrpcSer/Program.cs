using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Winton.Extensions.Configuration.Consul;

namespace Scanfer.Studio.GrpcSer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Environment.CurrentDirectory);
                    var path = Environment.CurrentDirectory;

                    config
                    .AddJsonFile(path + "/Config/Config.json", true, true);


                    #region Ìí¼Óconsul Ö§³Ö
                    config.AddConsul("MscfgOptions", options =>
                    {
                        options.ConsulConfigurationOptions = opts =>
                        {
                            opts.Address = new Uri("http://192.168.200.112:8500/");
                        };
                        options.ReloadOnChange = true;
                        options.Optional = true;

                    });
                    #endregion
                })

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        // Setup a HTTP/2 endpoint without TLS.
                        //options.ListenLocalhost(8888, o => o.Protocols =
                        //    HttpProtocols.Http1);
                        options.Listen(System.Net.IPAddress.Parse("192.168.1.230"), 8888, o => o.Protocols = HttpProtocols.Http1);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
