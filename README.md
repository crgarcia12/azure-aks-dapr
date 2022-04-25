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

