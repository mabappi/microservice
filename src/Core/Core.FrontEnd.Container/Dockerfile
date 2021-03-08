#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs
# End Install

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs
# End Install
WORKDIR /src

COPY ["Core.FrontEnd.Container/Core.FrontEnd.Container.csproj", "Core.FrontEnd.Container/"]
RUN dotnet restore "Core.FrontEnd.Container/Core.FrontEnd.Container.csproj"
COPY . .
WORKDIR "/src/Core.FrontEnd.Container"
RUN dotnet build "Core.FrontEnd.Container.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core.FrontEnd.Container.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.FrontEnd.Container.dll"]