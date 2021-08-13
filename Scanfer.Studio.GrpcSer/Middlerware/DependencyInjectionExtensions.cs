using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scanfer.Studio.GrpcSer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scanfer.Studio.GrpcSer.Middlerware
{
    public static class DependencyInjectionExtensions
    {

        /// <summary>
        /// 添加微服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configureOptions"></param>
        public static void AddMscfg(this IServiceCollection services, IConfiguration configuration, Action<MscfgOptions> configureOptions = null)
        {
            services.Configure<MscfgOptions>(configuration.GetSection("MscfgOptions"));
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }


            services.Configure<MscfgMonotorOptions>(configuration.GetSection("MscfgOptions"));
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }


            services.Configure<MscfgSnapshotOptions>(configuration.GetSection("MscfgOptions"));
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }

        }


        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://192.168.200.112:8500/"));//请求注册的 Consul 地址

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://192.168.1.230:8888/Health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5),
                TLSSkipVerify = true,
                
            };


            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = "Scanfer.Studio.GrpcSer_5001",
                Name = "Scanfer.Studio.GrpcSer",
                Address = "192.168.1.230",
                Port = 8888,
                Tags = new[] { $"urlprefix-/Scanfer.Studio.GrpcSer" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait(5000);//服务停止时取消注册
            });

            return app;

        }



    }
}
