$message = $args[0];
echo $message

if([string]::IsNullOrEmpty($message)){
    echo ""
    echo "Please input migration message that pass through first argument."
    echo "e.g.)"
    echo "PS> add-migration.ps1 'My first migration'"
    echo ""
}
else{
    cd ./src/Bing.Wallpaper.Data

    dotnet ef migrations add $message --context DefaultDbContext --startup-project ../Bing.Wallpaper --project ../Bing.Wallpaper.Data.SqlServer --json

    cd ../../
}