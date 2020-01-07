FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "Services/UserService/User.Host/User.Host.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone
COPY --from=build /app/publish .
CMD ["dotnet","User.Host.dll"]