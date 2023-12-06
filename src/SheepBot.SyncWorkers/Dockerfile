FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SheepBot.SyncWorkers/SheepBot.SyncWorkers.csproj", "SheepBot.SyncWorkers/"]
RUN dotnet restore "SheepBot.SyncWorkers/SheepBot.SyncWorkers.csproj"
COPY . .
WORKDIR "/src/SheepBot.SyncWorkers"
RUN dotnet build "SheepBot.SyncWorkers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SheepBot.SyncWorkers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SheepBot.SyncWorkers.dll"]
