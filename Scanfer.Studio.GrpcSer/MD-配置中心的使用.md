首先，我们要搭建一个Consul。访问端口为8500。

在我们的项目中引用包 Winton.Extensions.Configuration.Consul 这是用于.net core 的扩展。
然后我们在program.cs文件中加入以下内容：
                    #region 添加consul 支持
                    config.AddConsul("MscfgOptions", options =>
                    {
                        options.ConsulConfigurationOptions = opts =>
                        {
                            opts.Address = new Uri("http://192.168.200.112:8500/");
                        };
                        options.ReloadOnChange = true;
                        
                    });
                    #endregion

这样我们就指定了Consul服务配置中的位置，以及设置了他可以重新加载。

然后，我们将Config.json中的内容一起COPY到Consul的KEY/VALUE中去就可以了。
在程序运行的过程中，我们对Consul中的更改都会同步到本地的环境中来。非常好用的功能。

其他的和本地的OptionMonitor一样的使用。
记住一点的就是，Options是无法更新的。只有OptionsMonitor和Snapshot两种方式可以。
