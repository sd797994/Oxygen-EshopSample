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
kubectl apply -f k8sSample-dev.yaml