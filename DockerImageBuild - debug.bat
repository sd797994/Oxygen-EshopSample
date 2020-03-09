kubectl delete -f ./k8s/debug.yaml
cd ./ApiGateWay/
docker build . -t apigateway -f DockerfileDebug
cd ../Services/CommonService/JobRunner/
docker build . -t jobrunner -f DockerfileDebug
cd ../../UserService/User.Host/
docker build . -t userservice -f DockerfileDebug
cd ../../GoodsService/Goods.Host/
docker build . -t goodsservice -f DockerfileDebug
cd ../../OrderService/Order.Host/
docker build . -t orderservice -f DockerfileDebug
cd ../../TradeService/Trade.Host/
docker build . -t tradeservice -f DockerfileDebug
docker image prune -f

cd ../../../frontend
docker rmi frontend
call npm run build
docker build . -t frontend
docker image prune -f