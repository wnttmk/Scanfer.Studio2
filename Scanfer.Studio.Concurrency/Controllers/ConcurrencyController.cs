using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ConcurrencyLimiter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Scanfer.Studio.Concurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcurrencyController : ControllerBase
    {

        SemaphoreSlim semaphoreSlim;

        Semaphore semaphore;

        //IQueuePolicy queuePolicy;
        public ConcurrencyController(IQueuePolicy queuePolicy)
        {
            //this.queuePolicy = queuePolicy;
            semaphoreSlim = new SemaphoreSlim(2, 5);

            semaphore = new Semaphore(2, 5);
        }

        [HttpGet("Test")]
        public async Task<string> Test()
        {
            List<string> context = new List<string>();

            for (int i = 0; i < 10; i++)
            {


                var j = i;
                //Task.Run(async () =>
                // {
                //     Console.WriteLine("当前空余线程数：" + semaphoreSlim.CurrentCount);
                //     await semaphoreSlim.WaitAsync();
                //     Thread.Sleep(1000);

                //     //context.Add(i + " ->执行完成");
                //     semaphoreSlim.Release();
                //     Console.WriteLine(j + " ->执行完成,当前线程数：" + semaphoreSlim.CurrentCount);
                // });


                Task.Run(() =>
                {

                    bool isWait = semaphore.WaitOne(500);
                    if (!isWait)
                    {
                        Console.WriteLine(j + " -> 超时退出");
                        return;
                    }
                    Thread.Sleep(1000);
                    Console.WriteLine(j + " ->执行完成");

                });

            }
            await Task.CompletedTask;
            return string.Join("<br />", context);
        }

    }
}
