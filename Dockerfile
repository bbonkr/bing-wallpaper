FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# install node.js
RUN curl -sL https://deb.nodesource.com/setup_16.x |  bash -
RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY . .

# Fix dotnet restore
# RUN curl -o /usr/local/share/ca-certificates/verisign.crt -SsL https://crt.sh/?d=1039083 && update-ca-certificates

# npm update 
# RUN npm install -g npm

# Build client
RUN cd src/Bing.Wallpaper/ClientApp && npm ci && npm run build

# RUN dotnet restore
# copy everything else and build app
RUN cd src/Bing.Wallpaper && dotnet restore && dotnet publish -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

RUN mkdir -p /app/images
RUN mkdir -p /app/thumbnails

ENTRYPOINT ["dotnet", "Bing.Wallpaper.dll"]