FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
#WORKDIR "/src/Budtraget.Api"
COPY ["Budtraget.Api/Budtraget.Api.csproj", "."]
RUN dotnet restore "Budtraget.Api.csproj"

COPY . .

RUN dotnet build "Budtraget.Api.csproj" -c $configuration -o /app/build /p:UseAppHost=false
RUN pwd
RUN ls

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Budtraget.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get update && apt-get install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
ENTRYPOINT ["dotnet", "Budtraget.Api.dll"]
