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
### 附录
本地k8s环境安装简易教程
* 首先确保您的机器是windows10
  通过https://www.docker.com/products/docker-desktop 下载最新版本的docker for windows 社区版本
  安装并启动docker-ce ，在任务栏点击docker图标，并发右键Switch to Linux Containers切换到linux平台
  在任务栏点击docker图标，点击About Docker Desktop 查看Kubernetes 所要求的版本
  下载kubernetst 所需要的镜像
  ```bash
  git clone https://github.com/AliyunContainerService/k8s-for-docker-desktop.git
  cd k8s-for-docker-desktop
  .\load_images.ps1
```
  *请确保git分支和docker-ce要求的k8s版本一致，否则会导致k8s无法启动（如果git对应的分支没有包含您需要的版本，则需要自行通过国内镜像源下载所需镜像）
  查看docker镜像
  ```docker images```
  ```bash
k8s.gcr.io/kube-apiserver              v1.14.8       1e94481e8f30        2 months ago        209MB
k8s.gcr.io/kube-proxyr                 v1.14.8       849af609e0c6        2 months ago        82.1MB
k8s.gcr.io/kube-schedulerr             v1.14.8       f1e3e5f9f93e        2 months ago        81.6MB
k8s.gcr.io/kube-controller-managerr    v1.14.8       36a8001a79fd        2 months ago        158MB
docker/kube-compose-controllerr        v0.4.23       a8c3d87a58e7        7 months ago        35.3MB
docker/kube-compose-api-serverr        v0.4.23       f3591b2cb223        7 months ago        49.9MB
k8s.gcr.io/corednsr                    1.3.1         eb516548c180        11 months ago       40.3MB
k8s.gcr.io/etcdr                       3.3.10        2c4adeb21b4f        13 months ago       258MB
k8s.gcr.io/pauser                      3.1           da86e6ba6ca1        2 years ago         742kB
  ```
  在任务栏点击docker图标 -> setting -> Kubernets -> 勾选Enable Kubernetes -> Apply 并等待k8s环境启动完成
  在cmd输入```kubectl get node```如下所示则表示k8s已经成功启动:
   ```bash
  NAME             STATUS   ROLES    AGE   VERSION
docker-desktop   Ready    master   1s   v1.14.8
```
## License

MIT

[1]: https://github.com/sd797994/Oxygen/tree/dev-k8s "Oxygen"
[2]: https://www.jianshu.com/p/c726ed03562a "这里"
[3]: https://www.github.com/dotnetcore/cap "CAP"