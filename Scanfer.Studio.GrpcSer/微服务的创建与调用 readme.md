这里实现了一个简单的GRPC调用。
首先，我们在服务端（Scanfer.Studio.GrpcSer）创建了一个Protos\GrpcSer的文件。在这里面的可以申明一些方法。
我们引用了GRPC的包之后，在csproj文件中包含进去，将他的GrpcServices设置为Server。这样就是一个服务端了。
  #
  <ItemGroup>
	  <Protobuf Include="Protos\*.proto" GrpcServices="Server" />
  </ItemGroup>
  #

  这个时候，VS会自动给你生成一个抽像的基础类。
  我们再在Services文件夹中创建我们的实现类，继承并实现了就好。
  #
  public class GrpcService : GrpcSer.GrpcNService.GrpcNServiceBase
  #

  最后，我们还要在startup中注册一下这个微服务
  #
              app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapGrpcService<Services.GrpcService>();
                endpoints.MapGrpcService<StreamBuferService>();

  #

  然后，我们在客户端Scanfer.Studio.GrpcCli,将protos中的文件复制进去。
  并在csproj文件中包含进去，将他的GrpcServices设置为Client，这样就创建了一个客户端的基本类了
  #
  	<ItemGroup>
		<Protobuf Include="Protos\*.proto" GrpcServices="Client" />
	</ItemGroup>
  #

  然后，我们在API中进行调用。
  #
            GrpcChannel grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
            GrpcSer.GrpcNService.GrpcNServiceClient client = new GrpcNService.GrpcNServiceClient(grpcChannel);
  #


  另外，GRPC包括了，简单调用，单双流调用、双向流调用。
  在strbufer服务的AtoN中。我们试着写了一个双向流的调用。并通过CancellationTokenSource对调用服务的请求做了一个可以取消的TOKEN。

