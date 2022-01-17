#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Frontends/ContactsGuide.CoreMvcWebUI/ContactsGuide.CoreMvcWebUI.csproj", "src/Frontends/ContactsGuide.CoreMvcWebUI/"]
RUN dotnet restore "src/Frontends/ContactsGuide.CoreMvcWebUI/ContactsGuide.CoreMvcWebUI.csproj"
COPY . .
WORKDIR "/src/src/Frontends/ContactsGuide.CoreMvcWebUI"
RUN dotnet build "ContactsGuide.CoreMvcWebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactsGuide.CoreMvcWebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsGuide.CoreMvcWebUI.dll"]
