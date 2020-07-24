FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NiceOne.Client/NiceOne.Client.csproj", "NiceOne.Client/"]
COPY ["NiceOne/NiceOne.csproj", "NiceOne/"]
RUN dotnet restore "NiceOne.Client/NiceOne.Client.csproj"
COPY . .
WORKDIR "/src/NiceOne.Client"
RUN dotnet build "NiceOne.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NiceOne.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NiceOne.Client.dll"]