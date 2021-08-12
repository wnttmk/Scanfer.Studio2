using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
