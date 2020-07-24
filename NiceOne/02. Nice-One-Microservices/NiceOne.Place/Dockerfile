FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NiceOne.Place/NiceOne.Place.csproj", "NiceOne.Place/"]
COPY ["NiceOne/NiceOne.csproj", "NiceOne/"]
RUN dotnet restore "NiceOne.Place/NiceOne.Place.csproj"
COPY . .
WORKDIR "/src/NiceOne.Place"
RUN dotnet build "NiceOne.Place.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NiceOne.Place.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NiceOne.Place.dll"]