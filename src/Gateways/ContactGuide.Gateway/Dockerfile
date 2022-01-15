#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Gateways/ContactGuide.Gateway/ContactGuide.Gateway.csproj", "src/Gateways/ContactGuide.Gateway/"]
RUN dotnet restore "src/Gateways/ContactGuide.Gateway/ContactGuide.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Gateways/ContactGuide.Gateway"
RUN dotnet build "ContactGuide.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactGuide.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactGuide.Gateway.dll"]
