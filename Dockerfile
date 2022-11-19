FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/server/Migrator/Migrator.csproj", "server/Migrator/"]
RUN dotnet restore "/src/server/Migrator/Migrator.csproj"
COPY . .
WORKDIR "/src/server/Migrator"
RUN dotnet build "/src/server/Migrator/Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migrator.dll"]