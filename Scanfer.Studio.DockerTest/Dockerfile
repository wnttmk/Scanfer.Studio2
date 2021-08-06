
# 这句有问题，所以我先拉取了容器。然后再操作

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

EXPOSE 80

COPY . .

ENTRYPOINT ["dotnet", "Scanfer.Studio.DockerTest.dll"]
