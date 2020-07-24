FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NiceOne.Identity/NiceOne.Identity.csproj", "NiceOne.Identity/"]
COPY ["NiceOne/NiceOne.csproj", "NiceOne/"]
RUN dotnet restore "NiceOne.Identity/NiceOne.Identity.csproj"
COPY . .
WORKDIR "/src/NiceOne.Identity"
RUN dotnet build "NiceOne.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NiceOne.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NiceOne.Identity.dll"]