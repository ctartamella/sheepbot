FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sheep.Bot/SheepBot.csproj", "Sheep.Bot/"]
RUN dotnet restore "Sheep.Bot/SheepBot.csproj"
COPY . .
WORKDIR "/src/Sheep.Bot"
RUN dotnet build "SheepBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SheepBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SheepBot.dll"]
