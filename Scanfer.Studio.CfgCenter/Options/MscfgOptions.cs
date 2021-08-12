using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scanfer.Studio.CfgCenter.Options
{
    /// <summary>
    /// 微服务配置信息
    /// </summary>
    public class MscfgOptions : IOptions<MscfgOptions>
    {

        public string AppName { set; get; }

        public MscfgOptions Value => this;
    }
}
