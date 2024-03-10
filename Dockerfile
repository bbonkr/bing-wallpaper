FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base

WORKDIR /app 
EXPOSE 5000

# Runtime configuration options for globalization
# https://docs.microsoft.com/en-us/dotnet/core/runtime-config/globalization
ENV DOTNET_RUNNING_IN_CONTAINER 1
ENV ASPNETCORE_URLS=http://+:5000

# FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src

# install node.js
# https://github.com/nodesource/distributions?tab=readme-ov-file#using-ubuntu-1
RUN curl -fsSL https://deb.nodesource.com/setup_18.x |  bash - &&\ 
    apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY . .

# Build client
RUN cd src/Bing.Wallpaper/ClientApp &&\
    npm ci &&\ 
    npm run build

# RUN dotnet publish
RUN dotnet restore src/Bing.Wallpaper/Bing.Wallpaper.csproj &&\ 
    dotnet publish src/Bing.Wallpaper/Bing.Wallpaper.csproj -c Release -o ./out \
    --runtime linux-x64 \
    --no-self-contained

FROM base AS final
WORKDIR /app
COPY --from=build /src/out .

RUN mkdir -p /app/images
RUN mkdir -p /app/thumbnails

ENTRYPOINT ["dotnet", "Bing.Wallpaper.dll"]
