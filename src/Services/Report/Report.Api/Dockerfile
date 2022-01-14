#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Report/Report.Api/Report.Api.csproj", "src/Services/Report/Report.Api/"]
COPY ["src/Shared/ContactGuide.Shared/ContactGuide.Shared.csproj", "src/Shared/ContactGuide.Shared/"]
COPY ["src/Services/Report/Report.Domain/Report.Domain.csproj", "src/Services/Report/Report.Domain/"]
COPY ["src/Services/Report/Report.Repository/Report.Repository.csproj", "src/Services/Report/Report.Repository/"]
RUN dotnet restore "src/Services/Report/Report.Api/Report.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Report/Report.Api"
RUN dotnet build "Report.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Report.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Report.Api.dll"]
