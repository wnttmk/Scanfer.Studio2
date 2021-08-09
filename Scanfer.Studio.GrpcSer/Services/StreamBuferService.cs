using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scanfer.Studio.GrpcSer
{
    public class StreamBuferService : GrpcSer.strbufer.strbuferBase
    {
        /// <summary>
        /// A 发送给 NULL
        /// </summary>
        /// <param name="requestStream"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task AtoN(IAsyncStreamReader<ContentRequest> requestStream, IServerStreamWriter<ContentResponse> responseStream, ServerCallContext context)
        {
            var bathQueue = new Queue<string>();
            while (!context.CancellationToken.IsCancellationRequested && await requestStream.MoveNext())
            {
                var node = requestStream.Current.Id;
                bathQueue.Enqueue(node);
            }

            string id = null;
            while (!context.CancellationToken.IsCancellationRequested && bathQueue.TryDequeue(out id))
            {
                await responseStream.WriteAsync(new ContentResponse()
                {
                    Id = id,
                    Code = 200,
                    Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });

                await Task.Delay(800);
                Console.WriteLine("处理了 " + id);
            }

            //return base.AtoN(requestStream, responseStream, context);
        }
    }
}
