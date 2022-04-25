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

```
.Net Generate assets for build & debug
Dapr: Scafold dapr Task
Debug: Select and start deb Debug Configu
```

https://docs.dapr.io/developing-applications/ides/vscode/vscode-dapr-extension/#scaffold-dapr-debugging-tasks