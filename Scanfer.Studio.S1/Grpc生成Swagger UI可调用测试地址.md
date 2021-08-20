使用 Swagger 来生成测试文档
首先，需要将google中的两个proto文件COPY到项目中来。同时引用 Microsoft.AspNetCore.Grpc.Swagger 。
Microsoft.AspNetCore.Grpc.Swagger 是一个预发布版，所以一定要勾选一下。

然后在我们的proto文件中，加入

#
import "google/api/annotations.proto";
#

同时在方法体中加入
#
   option (google.api.http) = {
      get: "/v1/greeter/{name}"
    };
#

这个时候其实已经可以通过URL调用了。

但是我们需要加入一个UI。
SO，在StartUp中加入
#
            services.AddGrpcHttpApi();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddGrpcSwagger();

#
#
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

#

打完收工。
