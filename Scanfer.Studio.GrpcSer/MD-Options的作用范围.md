IOptions<>是单例，因此一旦生成了，除非通过代码的方式更改，它的值是不会更新的。
IOptionsMonitor<>也是单例，但是它通过IOptionsChangeTokenSource<> 能够和配置文件一起更新，也能通过代码的方式更改值。
IOptionsSnapshot<>是范围，所以在配置文件更新的下一次访问，它的值会更新，但是它不能跨范围通过代码的方式更改值，只能在当前范围（请求）内有效。

我们在启动文件中加入了JSON文件
#
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Environment.CurrentDirectory);
                    var path = Environment.CurrentDirectory;
                    config
                    .AddJsonFile(path + "/Config/Config.json", true, true);
                })
#

我们通过配置Configuration的方法分别对Options、Monitor、Snapshot进行了配置。
服务端在GrpcService.ReadAppName中再次分别读出来。
第一次得到的结果是一致的
然后修改Config.json
发现Options的没有变化，但是Monitor和Snapshot都变成了最新的。
#
        public override Task<ReadAppNameResponse> ReadAppName(ReadAppNameRequest request, ServerCallContext context) => Task.FromResult(new ReadAppNameResponse()
        {
            AppName = this.mscfgOptions.AppName,
            MonitorName = this.mscfgMonotorOptions.AppName,
            SnapshotName = this.mscfgSnapshotOptions.AppName
        });
#


Snapshot 的据说是在一个作用域 Scop 中可以变化，超过这个花括号就不会变化。我没有做测试。但是代码如下
#
        using(var scope = provider.CreateScope())
        {
            var sp = scope.ServiceProvider;
            var options1 = sp.GetRequiredService<IOptions<TestOptions>>();
            var options2 = sp.GetRequiredService<IOptionsMonitor<TestOptions>>();
            var options3 = sp.GetRequiredService<IOptionsSnapshot<TestOptions>>();
#
