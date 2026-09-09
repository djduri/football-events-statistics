FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG NUGET_FootballEvents_INTERNAL_ADDRESS
ARG NUGET_FootballEventsNAVI_INTERNAL_ADDRESS
ARG NUGET_INTERNAL_USER
ARG NUGET_INTERNAL_TOKEN

WORKDIR /src
COPY ["FootballEvents.API/FootballEvents.API.csproj", "FootballEvents.API/"]

# Copy the nuget.config template and replace the credentials with the provided parameters
COPY ["nuget.template.config", "nuget.template.config"]
RUN sed -i -e "s|USER|$NUGET_INTERNAL_USER|g" nuget.template.config
RUN sed -i -e "s|TOKEN|$NUGET_INTERNAL_TOKEN|g" nuget.template.config
RUN sed -i -e "s|FootballEvents_ADDRESS|$NUGET_FootballEvents_INTERNAL_ADDRESS|g" nuget.template.config
RUN sed -i -e "s|FootballEventsNAVI_ADDRESS|$NUGET_FootballEventsNAVI_INTERNAL_ADDRESS|g" nuget.template.config
RUN cp nuget.template.config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore "./FootballEvents.API/./FootballEvents.API.csproj"

COPY . .
WORKDIR "/src/FootballEvents.API"

RUN dotnet build "./FootballEvents.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG CONFIGS_URL

RUN dotnet publish "./FootballEvents.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Downloading appsettings from configs service
ADD --chmod=777 --chown=app:app $CONFIGS_URL /app/publish/appsettings.json

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN chmod 777 appsettings.json
RUN chown app:app appsettings.json

HEALTHCHECK --interval=4s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "FootballEvents.API.dll"]