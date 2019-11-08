FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["collegues-api/collegues-api.csproj", "collegues-api/"]
RUN dotnet restore "collegues-api/collegues-api.csproj"
COPY . .
WORKDIR "/src/collegues-api"
RUN dotnet build "collegues-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "collegues-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "collegues-api.dll"]