# Dockerfile

# Image de base pour le runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5241
EXPOSE 7260

# Image de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie des fichiers projet et restauration des dépendances
COPY ["TaskManagement.API/TaskManagement.API.csproj", "TaskManagement.API/"]
COPY ["TaskManagement.Infrastructure/TaskManagement.Infrastructure.csproj", "TaskManagement.Infrastructure/"]
COPY ["TaskManagement.Application/TaskManagement.Application.csproj", "TaskManagement.Application/"]
COPY ["TaskManagement.Domain/TaskManagement.Domain.csproj", "TaskManagement.Domain/"]
RUN dotnet restore "TaskManagement.API/TaskManagement.API.csproj"

# Copie de tout le code source
COPY . .

# Build
WORKDIR "/src/TaskManagement.API"
RUN dotnet build "TaskManagement.API.csproj" -c Release -o /app/build



# Image de publication
FROM build AS publish
WORKDIR /src/TaskManagement.API
RUN dotnet publish "TaskManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Image finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /home/app/.aspnet/DataProtection-Keys
ENTRYPOINT ["dotnet", "TaskManagement.API.dll"]
