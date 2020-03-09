FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone
COPY /publish .
CMD ["dotnet","Goods.Host.dll"]