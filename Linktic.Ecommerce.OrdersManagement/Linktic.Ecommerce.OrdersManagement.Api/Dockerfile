FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Linktic.Ecommerce.OrdersManagement.Api/Linktic.Ecommerce.OrdersManagement.Api.csproj", "Linktic.Ecommerce.OrdersManagement.Api/"]
RUN dotnet restore "Linktic.Ecommerce.OrdersManagement.Api/Linktic.Ecommerce.OrdersManagement.Api.csproj"
COPY . .
WORKDIR "/src/Linktic.Ecommerce.OrdersManagement.Api"
RUN dotnet build "Linktic.Ecommerce.OrdersManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Linktic.Ecommerce.OrdersManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Linktic.Ecommerce.OrdersManagement.Api.dll"]
