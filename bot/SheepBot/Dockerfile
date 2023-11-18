 # syntax=docker/dockerfile:1
ARG BUILD_MODE=Release

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS restore
WORKDIR /src
COPY SheepBot.sln ./
COPY SheepBot/SheepBot.csproj ./SheepBot/
COPY SheepBot.Models/SheepBot.Models.csproj ./SheepBot.Models/
COPY SheepBot.Repositories/SheepBot.Repositories.csproj ./SheepBot.Repositories/
COPY SheepBot.Services/SheepBot.Services.csproj ./SheepBot.Services/

RUN dotnet restore "SheepBot/SheepBot.csproj"

FROM restore as build
COPY . .

WORKDIR /src/SheepBot
ARG BUILD_MODE

RUN dotnet build SheepBot.csproj --no-restore -c ${BUILD_MODE} -o /app/build

FROM build AS publish
ARG BUILD_MODE

RUN dotnet publish SheepBot.csproj -c ${BUILD_MODE} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SheepBot.dll"]
