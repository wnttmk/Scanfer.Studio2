using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Scanfer.Studio.GrpcSer;

namespace Scanfer.Studio.GrpcCli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrpcCallController : ControllerBase
    {

        [HttpGet("Index")]
        public JsonResult Index()
        {
            GrpcChannel grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
            GrpcSer.GrpcService.GrpcServiceClient client = new GrpcService.GrpcServiceClient(grpcChannel);
            var result = client.GetServiceTime(new GetServiceTimeRequest()
            {
                Key = "this is Client Call:" + DateTime.Now

            });
            return new JsonResult(result);
        }
    }
}
