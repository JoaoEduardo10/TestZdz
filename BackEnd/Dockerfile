FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BackEndSolution.sln", "."]
COPY ["Core/Core.csproj", "Core/"]
COPY ["UseCase/UseCase.csproj", "UseCase/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["InfrastructureTest/InfrastructureTest.csproj", "InfrastructureTest/"]

RUN dotnet restore "BackEndSolution.sln"

COPY . .

WORKDIR "/src/Infrastructure"
RUN dotnet build "Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
