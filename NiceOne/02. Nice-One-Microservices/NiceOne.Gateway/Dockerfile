FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NiceOne.Gateway/NiceOne.Gateway.csproj", "NiceOne.Gateway/"]
COPY ["NiceOne/NiceOne.csproj", "NiceOne/"]
RUN dotnet restore "NiceOne.Gateway/NiceOne.Gateway.csproj"
COPY . .
WORKDIR "/src/NiceOne.Gateway"
RUN dotnet build "NiceOne.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NiceOne.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NiceOne.Gateway.dll"]