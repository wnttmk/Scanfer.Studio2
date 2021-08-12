using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scanfer.Studio.GrpcSer.Options
{
    /// <summary>
    /// 微服务配置信息
    /// </summary>
    public class MscfgOptions : IOptions<MscfgOptions>
    {

        /// <summary>
        /// 站点名称
        /// </summary>
        public string AppName { set; get; }

        public MscfgOptions Value => this;
    }

    public class MscfgMonotorOptions : IOptionsMonitor<MscfgMonotorOptions>
    {

        /// <summary>
        /// 站点名称
        /// </summary>
        public string AppName { set; get; }

        public MscfgMonotorOptions CurrentValue => this;

        public MscfgMonotorOptions Get(string name)
        {
            throw new NotImplementedException();
        }

        public IDisposable OnChange(Action<MscfgMonotorOptions, string> listener)
        {
            throw new NotImplementedException();
        }
    }


    public class MscfgSnapshotOptions : IOptionsSnapshot<MscfgSnapshotOptions>
    {

        /// <summary>
        /// 站点名称
        /// </summary>
        public string AppName { set; get; }

        public MscfgSnapshotOptions Value => this;

        public MscfgSnapshotOptions Get(string name)
        {
            throw new NotImplementedException();
        }
    }
}
