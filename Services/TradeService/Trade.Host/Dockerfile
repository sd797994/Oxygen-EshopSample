FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone
COPY /pub .
CMD ["dotnet","Trade.Host.dll"]