kubectl delete -f k8sSample-dev.yaml
cd ../ApiGateWay
rd /s /q E:\dotnet_project\Oxygen-EshopSample\k8s\ApiGateWay\pub
dotnet publish -c Debug  -o ../k8s/ApiGateWay/pub
cd ../Services/CommonServce/JobRunner
rd /s /q E:\dotnet_project\Oxygen-EshopSample\k8s\JobRunner\pub
dotnet publish -c Debug  -o ../../../k8s/JobRunner/pub
cd ../../../Services/UserService/User.Host
rd /s /q E:\dotnet_project\Oxygen-EshopSample\k8s\UserService\pub
dotnet publish -c Debug  -o ../../../k8s/UserService/pub
cd ../../../Services/TradeService/Trade.Host
rd /s /q E:\dotnet_project\Oxygen-EshopSample\k8s\TradeService\pub
dotnet publish -c Debug  -o ../../../k8s/TradeService/pub
cd ../../../Services/GoodsService/Goods.Host
rd /s /q E:\dotnet_project\Oxygen-EshopSample\k8s\GoodsService\pub
dotnet publish -c Debug  -o ../../../k8s/GoodsService/pub
cd ../../../Services/OrderService/Order.Host
rd /s /q E:\dotnet_project\Oxygen-EshopSample\k8s\OrderService\pub
dotnet publish -c Debug  -o ../../../k8s/OrderService/pub
cd ../../../k8s/ApiGateWay
docker build . -t apigateway:latest
cd ../JobRunner/
docker build . -t jobrunner:latest
cd ../UserService
docker build . -t userservice:latest
cd ../TradeService
docker build . -t tradeservice:latest
cd ../GoodsService
docker build . -t goodsservice:latest
cd ../OrderService
docker build . -t orderservice:latest
docker image prune -f
cd ../
kubectl apply -f k8sSample-dev.yaml