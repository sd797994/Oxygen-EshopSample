kubectl apply -f ./k8s/baseinfr.yaml
kubectl delete -f ./k8s/release.yaml
istioctl kube-inject -f ./k8s/release.yaml | kubectl apply -f -
kubectl apply -f ./k8s/istiorule/.
