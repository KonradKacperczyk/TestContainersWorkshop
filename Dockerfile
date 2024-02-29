FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .


RUN dotnet restore "WebApplication1/TestcontainersApi.csproj"

RUN dotnet build "WebApplication1/TestcontainersApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication1/TestcontainersApi.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestcontainersApi.dll"]