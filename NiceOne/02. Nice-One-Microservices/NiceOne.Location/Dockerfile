FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NiceOne.Location/NiceOne.Location.csproj", "NiceOne.Location/"]
COPY ["NiceOne/NiceOne.csproj", "NiceOne/"]
RUN dotnet restore "NiceOne.Location/NiceOne.Location.csproj"
COPY . .
WORKDIR "/src/NiceOne.Location"
RUN dotnet build "NiceOne.Location.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NiceOne.Location.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NiceOne.Location.dll"]