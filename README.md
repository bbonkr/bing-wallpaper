EF Core 3 부터 전역 또는 로컬 도구로 설치되어야 합니다.

```bash
$ dotnet tool install --global dotnet-ef
```

```
$ dotnet ef
                     _/\__       
               ---==/    \\
         ___  ___   |.    \|\    
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 3.0.0

Usage: dotnet ef [options] [command]

Options:
  --version        Show version information
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

Commands:
  database    Commands to manage the database.
  dbcontext   Commands to manage DbContext types.
  migrations  Commands to manage migrations.

Use "dotnet ef [command] --help" for more information about a command.
```

```
$ dotnet ef migrations add --help

Usage: dotnet ef migrations add [arguments] [options]

Arguments:
  <NAME>  The name of the migration.

Options:
  -o|--output-dir <PATH>                 The directory (and sub-namespace) to use. Paths are relative to the project directory. Defaults to "Migrations".
  --json                                 Show JSON output.
  -c|--context <DBCONTEXT>               The DbContext to use.
  -p|--project <PROJECT>                 The project to use.
  -s|--startup-project <PROJECT>         The startup project to use.
  --framework <FRAMEWORK>                The target framework.
  --configuration <CONFIGURATION>        The configuration to use.
  --runtime <RUNTIME_IDENTIFIER>         The runtime to use.
  --msbuildprojectextensionspath <PATH>  The MSBuild project extensions path. Defaults to "obj".    
  --no-build                             Don't build the project. Only use this when the build is up-to-date.
  -h|--help                              Show help information
  -v|--verbose                           Show verbose output.
  --no-color                             Don't colorize output.
  --prefix-output                        Prefix output with level.
```


데이터베이스 마이그레이션 코드 작성

```
$ cd Bing.Wallpaper.Data
$ dotnet ef migrations add "Initialize" --context DefaultDatabaseContext --startup-project ..\Bing.Wallpaper\Bing.Wallpaper.csproj --project ..\Bing.Wallpaper.Data.SqlServer\Bing.Wallpaper.Data.SqlServer.csproj
```