using Microsoft.AspNetCore.ConcurrencyLimiter;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scanfer.Studio.Concurrency
{
    public class QueuePolicy : IQueuePolicy
    {

        int _maxConcurrentRequests;

        int _requestQueueLimit;

        SemaphoreSlim _serverSemaphore;

        /// <summary>
        ///     构造方法(初始化Queue策略)
        /// </summary>
        /// <param name="options"></param>
        public QueuePolicy(IOptions<QueuePolicyOptions> options)
        {
            _maxConcurrentRequests = options.Value.MaxConcurrentRequests;
            if (_maxConcurrentRequests <= 0)
            {
                throw new ArgumentException(nameof(_maxConcurrentRequests), "MaxConcurrentRequests must be a positive integer.");
            }

            _requestQueueLimit = options.Value.RequestQueueLimit;
            if (_requestQueueLimit < 0)
            {
                throw new ArgumentException(nameof(_requestQueueLimit), "The RequestQueueLimit cannot be a negative number.");
            }
            //使用 SemaphoreSlim 来限制任务最大个数
            _serverSemaphore = new SemaphoreSlim(_maxConcurrentRequests);
        }

        public void OnExit()
        {
            _serverSemaphore.Release();
        }

        public async ValueTask<bool> TryEnterAsync()
        {
            await Task.Yield();
            return _serverSemaphore.CurrentCount < _maxConcurrentRequests;
        }
    }
}
