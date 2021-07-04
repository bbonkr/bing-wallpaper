cd ./src/Bing.Wallpaper.Data

dotnet ef migrations remove --context DefaultDatabaseContext --startup-project ../Bing.Wallpaper --project ../Bing.Wallpaper.Data.SqlServer --json

cd ../../
