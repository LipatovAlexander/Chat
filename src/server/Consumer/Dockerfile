FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/server/Domain/Domain.csproj", "Domain/"]
COPY ["src/server/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["src/server/Consumer/Consumer.csproj", "Consumer/"]
RUN dotnet restore "Consumer/Consumer.csproj"
COPY . .
WORKDIR "/src/src/server/Consumer"
RUN dotnet build "Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumer.dll"]