FROM public.ecr.aws/lambda/dotnet:8 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR Linktic.Ecommerce.ProductsCatalog.Api
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish 

FROM base AS final
WORKDIR /var/task
COPY --from=publish /app/publish .
CMD [ "Linktic.Ecommerce.ProductsCatalog.Api::Linktic.Ecommerce.ProductsCatalog.Api.LambdaEntryPoint::FunctionHandlerAsync" ]