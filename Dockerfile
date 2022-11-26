FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY "dchv_api.csproj" .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "dchv_api.dll"]

EXPOSE 80 443