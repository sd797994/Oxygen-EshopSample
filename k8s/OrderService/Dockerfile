FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone && sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf
COPY pub/. .
CMD ["dotnet","Order.Host.dll"]