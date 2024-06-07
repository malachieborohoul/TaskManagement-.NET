FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base


# Changer les autorisations du répertoire de clés
RUN mkdir -p /home/app/.aspnet/DataProtection-Keys && chmod -R 777 /home/app/.aspnet/DataProtection-Keys

USER $APP_UID
WORKDIR /app
EXPOSE 5241
EXPOSE 7260

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskManagement.API/TaskManagement.API.csproj", "TaskManagement.API/"]
COPY ["TaskManagement.Infrastructure/TaskManagement.Infrastructure.csproj", "TaskManagement.Infrastructure/"]
COPY ["TaskManagement.Application/TaskManagement.Application.csproj", "TaskManagement.Application/"]
COPY ["TaskManagement.Domain/TaskManagement.Domain.csproj", "TaskManagement.Domain/"]
RUN dotnet restore "TaskManagement.API/TaskManagement.API.csproj"
COPY . .
WORKDIR "/src/TaskManagement.API"
RUN dotnet build "TaskManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /home/app/.aspnet/DataProtection-Keys  
ENTRYPOINT ["dotnet", "TaskManagement.API.dll"]
