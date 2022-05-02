# azure-aks-dapr

## Create an AKS cluster
az group create --name crgar-aks-sidecar-rg --location eastus
az aks create --resource-group crgar-aks-sidecar-rg --name crgar-aks-sidecar-aks --node-count 1 --enable-addons monitoring --generate-ssh-keys
az aks get-credentials --resource-group  crgar-aks-sidecar-rg --name crgar-aks-sidecar-aks --admin

## Create a dotnet app

dotnet new webapi
dotnet run

change:
```
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

by

```
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./swagger/v1/swagger.json", "DAPR API");
        c.RoutePrefix = "";
    });
// }

// app.UseHttpsRedirection();
```
## Add DAPR to the app

dotnet add package Dapr.AspNetCore

In Program.cs, add:
```
builder.Services.AddDaprClient();
```

dapr run --app-id myapp --app-port 5000 -- dotnet run
## Install DAPR in the cluster

dapr init --kubernetes
dapr status -k
dapr mtls -k
kubectl get pods --namespace dapr-system


## Debug DAPR in VSCode

```
.Net Generate assets for build & debug
Dapr: Scafold dapr Task
Debug: Select and start deb Debug Configu
```
https://docs.dapr.io/developing-applications/ides/vscode/vscode-dapr-extension/#scaffold-dapr-debugging-tasks

## Add a DOCKERFILE
-------------------------------------
Dockerfile
dotnet v6

az acr login -n crgarakspublicacr
docker build -t crgarakspublicacr.azurecr.io/daprapi:1.0.9 .
docker push crgarakspublicacr.azurecr.io/daprapi:1.0.9

-------------------------------------

## Deploy DAPR components

dapr dashboard -k -p 8082
dapr mtls renew-certificate 