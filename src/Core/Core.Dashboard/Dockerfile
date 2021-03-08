#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs

WORKDIR /src
COPY ["Core.Dashboard/Core.Dashboard.csproj", "Core.Dashboard/"]
RUN dotnet restore "Core.Dashboard/Core.Dashboard.csproj"
COPY . .
WORKDIR "/src/Core.Dashboard"
RUN dotnet build "Core.Dashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core.Dashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.Dashboard.dll"]