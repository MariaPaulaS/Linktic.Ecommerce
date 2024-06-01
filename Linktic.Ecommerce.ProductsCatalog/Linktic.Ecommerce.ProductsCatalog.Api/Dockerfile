FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Linktic.Ecommerce.ProductsCatalog.Api/Linktic.Ecommerce.ProductsCatalog.Api.csproj", "Linktic.Ecommerce.ProductsCatalog.Api/"]
RUN dotnet restore "Linktic.Ecommerce.ProductsCatalog.Api/Linktic.Ecommerce.ProductsCatalog.Api.csproj"
COPY . .
WORKDIR "/src/Linktic.Ecommerce.ProductsCatalog.Api"
RUN dotnet build "Linktic.Ecommerce.ProductsCatalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Linktic.Ecommerce.ProductsCatalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Linktic.Ecommerce.ProductsCatalog.Api.dll"]
