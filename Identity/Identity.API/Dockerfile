# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 6001
EXPOSE 7001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Identity/Identity.API/Identity.API.csproj", "Identity/Identity.API/"]
COPY ["Identity/Identity.Application/Identity.Application.csproj", "Identity/Identity.Application/"]
COPY ["Identity/Identity.Domain/Identity.Domain.csproj", "Identity/Identity.Domain/"]
COPY ["Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "Identity/Identity.Infrastructure/"]
COPY ["Identity/Identity.Presentation/Identity.Presentation.csproj", "Identity/Identity.Presentation/"]
COPY ["Base.API/Base.API.csproj", "Base.API/"]
COPY ["Base.Common/Base.Common.csproj", "Base.Common/"]
RUN dotnet restore "./Identity/Identity.API/Identity.API.csproj"
COPY . .
WORKDIR "/src/Identity/Identity.API"
RUN dotnet build "./Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]