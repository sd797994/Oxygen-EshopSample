kubectl delete -f ./k8s/release.yaml
cd ./ApiGateWay/
dotnet publish -c Release -o pub
docker rmi apigateway
docker build . -t apigateway -f Dockerfile

cd ../Services/CommonService/JobRunner/
dotnet publish -c Release -o pub
docker rmi jobrunner
docker build . -t jobrunner -f Dockerfile

cd ../../UserService/User.Host/
dotnet publish -c Release -o pub
docker rmi userservice
docker build . -t userservice -f Dockerfile

cd ../../GoodsService/Goods.Host/
dotnet publish -c Release -o pub
docker rmi goodsservice
docker build . -t goodsservice -f Dockerfile

cd ../../OrderService/Order.Host/
dotnet publish -c Release -o pub
docker rmi orderservice
docker build . -t orderservice -f Dockerfile

cd ../../TradeService/Trade.Host/
dotnet publish -c Release -o pub
docker rmi tradeservice
docker build . -t tradeservice -f Dockerfile

cd ../../../frontend
docker rmi frontend
call npm run build
docker build . -t frontend
docker image prune -f