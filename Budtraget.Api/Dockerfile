# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Budtraget.Api/Budtraget.Api.csproj", "."]
RUN dotnet restore "Budtraget.Api.csproj"
COPY . .
RUN dotnet publish "Budtraget.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Production-only optimizations
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
EXPOSE 8080

ENTRYPOINT ["dotnet", "Budtraget.Api.dll"]