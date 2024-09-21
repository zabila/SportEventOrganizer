FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Sportalytics.Feed.API/Sportalytics.Feed.API.csproj", "src/Sportalytics.Feed.API/"]
COPY ["src/Sportalytics.Feed.Application/Sportalytics.Feed.Application.csproj", "src/Sportalytics.Feed.Application/"]
COPY ["src/Sportalytics.Feed.Domain/Sportalytics.Feed.Domain.csproj", "src/Sportalytics.Feed.Domain/"]
COPY ["src/Sportalytics.Feed.Persistence.MongoDB/Sportalytics.Feed.Persistence.MongoDB.csproj", "src/Sportalytics.Feed.Persistence.MongoDB/"]
COPY ["src/Sportalytics.Feed.Persistence.PostgreSQL/Sportalytics.Feed.Persistence.PostgreSQL.csproj", "src/Sportalytics.Feed.Persistence.PostgreSQL/"]
COPY ["src/Sportalytics.Feed.Infrastructure/Sportalytics.Feed.Infrastructure.csproj", "src/Sportalytics.Feed.Infrastructure/"]
COPY ["src/Sportalytics.Feed.Infrastructure.Kafka/Sportalytics.Feed.Infrastructure.Kafka.csproj", "src/ Sportalytics.Feed.Infrastructure.Kafka/"]
COPY ["src/Sportalytics.Feed.Presentation/Sportalytics.Feed.Presentation.csproj", "src/Sportalytics.Feed.Presentation/"]
RUN dotnet restore "src/Sportalytics.Feed.API/Sportalytics.Feed.API.csproj"
COPY . .
WORKDIR "/src/src/Sportalytics.Feed.API"
RUN dotnet build "Sportalytics.Feed.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sportalytics.Feed.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sportalytics.Feed.API.dll"]
