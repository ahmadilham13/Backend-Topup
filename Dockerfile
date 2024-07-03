FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5039

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY *.sln .
COPY backend/backend.csproj /backend/backend.csproj
RUN dotnet restore backend/ --interactive
COPY backend/appsettings.json /backend/appsettings.json
COPY . .
# COPY script.sh BisDevSales/script.sh

ENV PATH=${PATH}:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef --version 8.0.3

RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY --from=build BisDevSales/script.sh /mnt/script/script.sh
# RUN chmod +x /mnt/script/script.sh && /mnt/script/script.sh

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backend.dll"]