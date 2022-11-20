FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/server/Migrator/Migrator.csproj", "Migrator/"]
RUN dotnet restore "Migrator/Migrator.csproj"
COPY . .
WORKDIR "/src/src/server/Migrator"
RUN dotnet build "Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migrator.dll"]