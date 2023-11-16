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
RUN dotnet build SheepBot.csproj --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish SheepBot.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SheepBot.dll"]
