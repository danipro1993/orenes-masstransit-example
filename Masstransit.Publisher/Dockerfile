FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Masstransit.Publisher/Masstransit.Publisher.csproj", "Masstransit.Publisher/"]
RUN dotnet restore "Masstransit.Publisher/Masstransit.Publisher.csproj"
COPY . .
WORKDIR "/src/Masstransit.Publisher"
RUN dotnet build "Masstransit.Publisher.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Masstransit.Publisher.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Masstransit.Publisher.dll"]
