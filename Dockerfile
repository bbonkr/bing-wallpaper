FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

WORKDIR /app
EXPOSE 5000

# Runtime configuration options for globalization
# https://docs.microsoft.com/en-us/dotnet/core/runtime-config/globalization
ENV DOTNET_RUNNING_IN_CONTAINER 1
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY . .

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
