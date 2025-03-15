# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Ensure ASP.NET listens on all interfaces
ENV ASPNETCORE_URLS=http://+:8080

# Use SDK for building the project
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy only the project file for dependency restore (improves caching)
COPY ["Budtraget.Api.csproj", "."]
RUN dotnet restore "Budtraget.Api.csproj"

# Copy the entire project source
COPY . .
WORKDIR "/src"

# Debugging - Check if files are present
RUN ls -l

# Build the application
RUN dotnet build "Budtraget.Api.csproj" -c $configuration -o /app/build

# Publish the application
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Budtraget.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Budtraget.Api.dll"]