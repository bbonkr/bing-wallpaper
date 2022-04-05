FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app 
EXPOSE 80
EXPOSE 443 

# Runtime configuration options for globalization
# https://docs.microsoft.com/en-us/dotnet/core/runtime-config/globalization
# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV DOTNET_RUNNING_IN_CONTAINER=true

# RUN apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# install node.js
RUN curl -sL https://deb.nodesource.com/setup_16.x |  bash -
RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY . .

# Fix dotnet restore
# RUN curl -o /usr/local/share/ca-certificates/verisign.crt -SsL https://crt.sh/?d=1039083 && update-ca-certificates

# Build client
RUN cd src/Bing.Wallpaper/ClientApp && npm ci && npm run build

# RUN dotnet publish
RUN cd src/Bing.Wallpaper && dotnet restore && dotnet publish -c Release -o /app/out \
    --runtime alpine-x64 
# --self-contained true \
# /p:PublishTrimmed=true \
# /p:PublishSingleFile=true


# FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
FROM base as final
WORKDIR /app
COPY --from=build /app/out ./

RUN mkdir -p /app/images
RUN mkdir -p /app/thumbnails

ENTRYPOINT ["dotnet", "Bing.Wallpaper.dll"]
# ENTRYPOINT ["./Bing.Wallpaper"]