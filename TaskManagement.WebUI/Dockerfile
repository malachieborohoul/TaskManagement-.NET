FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build



WORKDIR /src
COPY ["TaskManagement.WebUI/TaskManagement.WebUI.csproj", "TaskManagement.WebUI/"]
COPY ["TaskManagement.Domain/TaskManagement.Domain.csproj", "TaskManagement.Domain/"]
RUN dotnet restore "TaskManagement.WebUI/TaskManagement.WebUI.csproj"
COPY . .
WORKDIR "/src/TaskManagement.WebUI"
RUN dotnet build "TaskManagement.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskManagement.WebUI.csproj" -c Release -o /app/publish

# Our NGINX configurations
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY TaskManagement.WebUI/nginx.conf /etc/nginx/nginx.conf
COPY --from=publish /app/publish/wwwroot .
