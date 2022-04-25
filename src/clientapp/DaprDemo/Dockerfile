#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApplication20/WebApplication20.csproj", "WebApplication20/"]
RUN dotnet restore "WebApplication20/WebApplication20.csproj"
COPY . .
WORKDIR "/src/WebApplication20"
RUN dotnet build "WebApplication20.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication20.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication20.dll"]