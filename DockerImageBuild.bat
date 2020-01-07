kubectl delete -f ./k8s/release.yaml
docker build . -t apigateway -f ./ApiGateWay/Dockerfile
docker build . -t jobrunner -f ./Services/CommonService/JobRunner/Dockerfile
docker build . -t userservice -f ./Services/UserService/User.Host/Dockerfile
docker build . -t goodsservice -f ./Services/GoodsService/Goods.Host/Dockerfile
docker build . -t orderservice -f ./Services/OrderService/Order.Host/Dockerfile
docker build . -t tradeservice -f ./Services/TradeService/Trade.Host/Dockerfile
cd frontend
docker build . -t frontend
cd ../
docker image prune -f








