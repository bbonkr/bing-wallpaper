#!/bin/bash

message=$1

echo "Migrations name: $message"

if [ -z "$message" ]
then
    echo ""
    echo "Please input migration message that pass through first argument."
    echo "e.g.)"
    echo "$ ./ef-migration-add.sh 'My first migration'"
    echo ""
else
    cd ./src/Bing.Wallpaper.Data

    dotnet ef migrations add "$message" --context DefaultDatabaseContext --startup-project ../Bing.Wallpaper --project ../Bing.Wallpaper.Data.SqlServer --json

    cd ../../
fi 
