FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Budtraget.Api.csproj", "Budtraget.Api/"]
RUN dotnet restore "Budtraget.Api/Budtraget.Api.csproj"
WORKDIR "/src/Budtraget.Api"
COPY . .
RUN dotnet build "Budtraget.Api.csproj" -c $configuration -o /app/build

RUN pwd
RUN ls

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Budtraget.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Budtraget.Api.dll"]
