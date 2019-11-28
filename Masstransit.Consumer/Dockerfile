FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Masstransit.Consumer/Masstransit.Consumer.csproj", "Masstransit.Consumer/"]
RUN dotnet restore "Masstransit.Consumer/Masstransit.Consumer.csproj"
COPY . .
WORKDIR "/src/Masstransit.Consumer"
RUN dotnet build "Masstransit.Consumer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Masstransit.Consumer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Masstransit.Consumer.dll"]