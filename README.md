# azure-aks-dapr
dapr init --kubernetes
dapr status -k
dapr mtls -k

dotnet new webapi
comment //app.UseHttpsRedirection();
dotnet run

dotnet add package Dapr.AspNetCore

```
builder.Services.AddDaprClient();
```

```
controller code
```

dapr run --app-id myapp --app-port 5000 -- dotnet run

```
.Net Generate assets for build & debug
Dapr: Scafold dapr Task
Debug: Select and start deb Debug Configu
```

https://docs.dapr.io/developing-applications/ides/vscode/vscode-dapr-extension/#scaffold-dapr-debugging-tasks


-------------------------------------
Dockerfile
dotnet v6

az acr login -n crgarakspublicacr
docker build . -t crgarakspublicacr.azurecr.io/daprapi:1.0.0
docker push crgarakspublicacr.azurecr.io/daprapi:1.0.0

-------------------------------------
dapr uninstall -k
dapr init -k
dapr status -k
kubectl get pods --namespace dapr-system
dapr dashboard -k

dapr mtls renew-certificate 