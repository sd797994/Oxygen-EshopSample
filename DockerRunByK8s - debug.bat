kubectl apply -f ./k8s/baseinfr.yaml
kubectl delete -f ./k8s/debug.yaml
kubectl apply -f ./k8s/debug.yaml
