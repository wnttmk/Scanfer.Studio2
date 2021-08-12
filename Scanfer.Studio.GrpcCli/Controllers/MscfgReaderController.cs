using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Scanfer.Studio.GrpcCli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MscfgReaderController : ControllerBase
    {

        [HttpGet("Get")]
        public async Task<string> Get()
        {
            GrpcChannel grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
            GrpcSer.GrpcNService.GrpcNServiceClient client = new GrpcSer.GrpcNService.GrpcNServiceClient(grpcChannel);
            var result = await client.ReadAppNameAsync(new GrpcSer.ReadAppNameRequest());
            return await Task.FromResult(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

    }
}
