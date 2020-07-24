FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NiceOne.Notifications/NiceOne.Notifications.csproj", "NiceOne.Notifications/"]
COPY ["NiceOne/NiceOne.csproj", "NiceOne/"]
RUN dotnet restore "NiceOne.Notifications/NiceOne.Notifications.csproj"
COPY . .
WORKDIR "/src/NiceOne.Notifications"
RUN dotnet build "NiceOne.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NiceOne.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NiceOne.Notifications.dll"]