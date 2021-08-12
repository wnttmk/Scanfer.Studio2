using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Scanfer.Studio.GrpcSer.Services
{
    public class GrpcService : GrpcSer.GrpcNService.GrpcNServiceBase
    {
        Options.MscfgOptions mscfgOptions;

        Options.MscfgMonotorOptions mscfgMonotorOptions;

        Options.MscfgSnapshotOptions mscfgSnapshotOptions;

        public GrpcService(IOptions<Options.MscfgOptions> mscfgOpts, IOptionsMonitor<Options.MscfgMonotorOptions> optionsMonitor, IOptionsSnapshot<Options.MscfgSnapshotOptions> optionsSnapshot)
        {
            this.mscfgOptions = mscfgOpts.Value;

            this.mscfgMonotorOptions = optionsMonitor.CurrentValue;

            this.mscfgSnapshotOptions = optionsSnapshot.Value;
        }



        public override Task<GetServiceTimeResponse> GetServiceTime(GetServiceTimeRequest request, ServerCallContext context) =>
            Task.FromResult(new GetServiceTimeResponse()
            {
                Code = 200,
                CurrTime = DateTime.Now.ToString($"yyyy-MM-dd HH:mm:ss") + $" ->{request.Key}"

            });


        public override Task<ReadAppNameResponse> ReadAppName(ReadAppNameRequest request, ServerCallContext context)
        {

            return Task.FromResult(new ReadAppNameResponse()
            {
                AppName = this.mscfgOptions.AppName,
                MonitorName = this.mscfgMonotorOptions.AppName,
                SnapshotName = this.mscfgSnapshotOptions.AppName
            });
        }
    }
}
