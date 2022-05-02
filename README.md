# azure-aks-dapr

## Create an AKS cluster
az group create --name crgar-aks-sidecar-rg --location eastus
az aks create --resource-group crgar-aks-sidecar-rg --name crgar-aks-sidecar-aks --node-count 1 --enable-addons monitoring --generate-ssh-keys --attach-acr crgarakspublicacr

az aks get-credentials --resource-group  crgar-aks-sidecar-rg --name crgar-aks-sidecar-aks --admin

## Create a dotnet app

dotnet new webapi -n daprdemo
replace framework in -cs by v6
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
    app.UseDeveloperExceptionPage();
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
az acr login -n crgarakspublicacr
docker build -t crgarakspublicacr.azurecr.io/daprdemo:1.0.10 .
docker push crgarakspublicacr.azurecr.io/daprdemo:1.0.10
docker run -p 4444:80 crgarakspublicacr.azurecr.io/daprdemo:1.0.10

## Deploy the app and DAPR components
kubectl apply -f .\deployments\k8s.yaml

kubectl get pods
kubectl get services

kubectl apply -f .\deployments\k8s.yaml

dapr dashboard -k -p 8082
dapr mtls renew-certificate 