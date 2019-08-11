FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5080

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build

WORKDIR /src
COPY "MediaService/" "MediaService/"
COPY "Common/" "Common/"
RUN dotnet restore "MediaService/MediaService/MediaService/MediaService.csproj"

WORKDIR "/src/MediaService/MediaService/MediaService/"
RUN dotnet build "MediaService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MediaService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MediaService.dll"]
