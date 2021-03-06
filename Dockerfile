FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# COPY . .
# RUN dotnet restore

# copy everything else and build app
RUN cd Bing.Wallpaper && dotnet restore && dotnet publish -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Bing.Wallpaper.dll"]