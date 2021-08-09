using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace Scanfer.Studio.GrpcSer.Services
{
    public class GrpcService : GrpcSer.GrpcService.GrpcServiceBase
    {
        public override Task<GetServiceTimeResponse> GetServiceTime(GetServiceTimeRequest request, ServerCallContext context) =>
            Task.FromResult(new GetServiceTimeResponse()
            {
                Code = 200,
                CurrTime = DateTime.Now.ToString($"yyyy-MM-dd HH:mm:ss") + $" ->{request.Key}"

            });
    }
}
