# Oxygen-EshopSample
Oxygen-EshopSample 是一款基于.netcore3.1 的针对k8s平台的分布式服务编排框架([Oxygen][1])简易案例
## 系统要求

* window10 / centos7.5 +

* docker for windows 19.03 +

* kubernetes 1.14.8 + (docker for windows)
* dotnetcore3.1 + vs2019 + nuget
## 特色
* 独立的网关，实现了基本的jwt鉴权和聚合服务功能
* 基于k8s coredns的服务发现，无需安装第三方注册发现软件
* 基于([CAP][3])实现的事件总线，可方便的实现分布式事务
* 前端采用vue+vant实现了一个简易的电商案例
* 基于清洁模型的领域驱动实现
## 安装
* 通过git 克隆代码到本地:

```bash
git clone https://github.com/sd797994/Oxygen-EshopSample.git
cd Oxygen-EshopSample
```

* 执行bat文件创建k8s deployment services ingress(我们假定您的k8s环境已经准备就绪):
*k8s需要安装ingress-controller 推荐ingress-nginx。ingress安装可参考[这里][2]

```bash
.\DockerImageBuild.bat
.\DockerRunByK8s.bat
```
* 项目第一次初始化会下载默认的镜像，或者您可以提前下载需要的镜像
```bash
docker pull redis:latest
docker pull rabbitmq:latest
docker pull mcr.microsoft.com/mssql/server:2019-latest
```

* 修改Host文件写入以下配置
```bash
127.0.0.1 www.oxygen-eshopsample.com #前端域名
127.0.0.1 api.oxygen-eshopsample.com #接口域名
```
* 查询k8s pod 确保所有pod已经正常启动,访问www.oxygen-eshopsample.com

```bash
kubectl get pod
```
## License

MIT

[1]: https://www.github.com/sd797994/oxygen "Oxygen"
[2]: https://www.jianshu.com/p/c726ed03562a "这里"
[3]: https://www.github.com/dotnetcore/cap "CAP"