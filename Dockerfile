FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base

WORKDIR /app 
EXPOSE 5000

# Runtime configuration options for globalization
# https://docs.microsoft.com/en-us/dotnet/core/runtime-config/globalization
ENV DOTNET_RUNNING_IN_CONTAINER 1
ENV ASPNETCORE_URLS=http://+:5000

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS build
WORKDIR /app

# install node.js
RUN curl -sL https://deb.nodesource.com/setup_18.x |  bash -
RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY . .

# Build client
RUN cd src/Bing.Wallpaper/ClientApp && npm ci && npm run build

# RUN dotnet publish
RUN cd src/Bing.Wallpaper && dotnet restore && dotnet publish -c Release -o /app/out \
    --runtime linux-x64 \
    --no-self-contained

FROM base as final
WORKDIR /app
COPY --from=build /app/out ./

RUN mkdir -p /app/images
RUN mkdir -p /app/thumbnails

ENTRYPOINT ["dotnet", "Bing.Wallpaper.dll"]
