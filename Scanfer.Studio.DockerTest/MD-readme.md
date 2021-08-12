主要体现在Dockerfile上面
我们的FROM有问题。SO，我们可以先看一下Docker上面到底有哪些dotnet

# docker search dotnet

然后再构建。

一般来说，我们的build都是在本地进行。做成镜像后再推送到Docker本地仓库。
然后再在服务器上pull运行。


