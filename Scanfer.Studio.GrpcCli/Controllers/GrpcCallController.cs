using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Scanfer.Studio.GrpcSer;
using System.Threading;

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
            GrpcSer.GrpcNService.GrpcNServiceClient client = new GrpcNService.GrpcNServiceClient(grpcChannel);
            //GrpcSer.GrpcService.GrpcServiceClient client = new GrpcService.GrpcServiceClient(grpcChannel);
            var result = client.GetServiceTime(new GetServiceTimeRequest()
            {
                Key = "this is Client Call:" + DateTime.Now

            });
            return new JsonResult(result);
        }

        [HttpGet("SteamTest")]
        public async Task SteamTest()
        {
            GrpcChannel grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
            GrpcSer.strbufer.strbuferClient client = new strbufer.strbuferClient(grpcChannel);
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(3000));
            var batchResult = client.AtoN(cancellationToken: cts.Token);

            // 定义发送，向RequestStream中写入对象
            var i = 0;
            while (i < 10)
            {
                await batchResult.RequestStream.WriteAsync(new ContentRequest()
                {
                    Id = DateTime.Now.ToString("mm.ss")
                });
                i++;
                Console.WriteLine($"发送完成 ->{i}");
            }

            // 定义一个接收处理逻辑
            var batchRespTask = Task.Run(async () =>
           {
               try
               {
                   await foreach (var resp in batchResult.ResponseStream.ReadAllAsync())
                   {
                       Console.WriteLine(resp.Id + " -> " + resp.Time);
                   }
               }
               catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.Cancelled)
               {
                   Console.WriteLine("超出时最处理时间啦！~~~~~~~~~~~~~~~");
               }
           });
            // 先等待发送完成
            await batchResult.RequestStream.CompleteAsync();
            Console.WriteLine("发送完成########################");
            await batchRespTask;
            Console.WriteLine("执行完成########################");
        }
    }
}
