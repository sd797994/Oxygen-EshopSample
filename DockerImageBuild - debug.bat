kubectl delete -f ./k8s/debug.yaml
dotnet publish "ApiGateWay/ApiGateWay.csproj" -c Debug -o ApiGateWay/debugpublish
docker build . -t apigateway -f ./ApiGateWay/DockerfileDebug
dotnet publish "Services/CommonService/JobRunner/JobRunner.csproj" -c Debug -o Services/CommonService/JobRunner/debugpublish
docker build . -t jobrunner -f ./Services/CommonService/JobRunner/DockerfileDebug
dotnet publish "Services/UserService/User.Host/User.Host.csproj" -c Debug -o Services/UserService/User.Host/debugpublish
docker build . -t userservice -f ./Services/UserService/User.Host/DockerfileDebug
dotnet publish "Services/GoodsService/Goods.Host/Goods.Host.csproj" -c Debug -o Services/GoodsService/Goods.Host/debugpublish
docker build . -t goodsservice -f ./Services/GoodsService/Goods.Host/DockerfileDebug
dotnet publish "Services/OrderService/Order.Host/Order.Host.csproj" -c Debug -o Services/OrderService/Order.Host/debugpublish
docker build . -t orderservice -f ./Services/OrderService/Order.Host/DockerfileDebug
dotnet publish "Services/TradeService/Trade.Host/Trade.Host.csproj" -c Debug -o Services/TradeService/Trade.Host/debugpublish
docker build . -t tradeservice -f ./Services/TradeService/Trade.Host/DockerfileDebug
cd frontend
docker build . -t frontend
cd ../
docker image prune -f