 # syntax=docker/dockerfile:1
ARG BUILD_MODE=Release

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /app
COPY SheepBot.sln ./
COPY src/SheepBot/SheepBot.csproj ./src/SheepBot/
COPY src/SheepBot.Application/SheepBot.Application.csproj ./src/SheepBot.Application/
COPY src/SheepBot.Domain/SheepBot.Domain.csproj ./src/SheepBot.Domain/
COPY src/SheepBot.Infrastructure/SheepBot.Infrastructure.csproj ./src/SheepBot.Infrastructure/
COPY src/SheepBot.iRacing.Client/SheepBot.iRacing.Client.csproj ./src/SheepBot.iRacing.Client/

RUN dotnet restore "src/SheepBot/SheepBot.csproj"

FROM restore as build
COPY . .

WORKDIR /app/src/SheepBot
ARG BUILD_MODE

RUN dotnet publish SheepBot.csproj -c ${BUILD_MODE} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SheepBot.dll"]
