using Grpc.AspNetCore.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scanfer.Studio.CfgCenter.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scanfer.Studio.CfgCenter
{
    public static class DI
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

        }

        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder builder, Action<MscfgOptions> configure)
        {
            return builder;
        }

    }
}
