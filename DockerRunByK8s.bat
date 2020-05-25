kubectl apply -f ./k8s/baseinfr.yaml
kubectl delete -f ./k8s/release.yaml
kubectl delete -f ./k8s/k8singressrule/.
kubectl apply -f ./k8s/release.yaml
kubectl apply -f ./k8s/k8singressrule/.
kubectl delete deploy apigateway-dep-v2 userservice-v2