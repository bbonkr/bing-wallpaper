---
title: "Bing Today Image Collector"
---

# Bing Today Image Collector

Bing.com 오늘의 이미지를 수집합니다.

## 준비

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

## 실행

### 로컬에서 실행

원격 저장소으 파일을 로컬 저장소로 복제합니다.

```bash
$ cd Bing.Wallpaper.Service.App
$ dotnet run
```

### 도커 컨테이너로 실행

원격 저장소으 파일을 로컬 저장소로 복제합니다.

```bash
$ touch .env
```

`.env` 파일을 열고 파일을 저장할 경로를 입력합니다.

```
DESTINATION_PATH=/home/my/images
```

도커 컴포즈를 사용해서 컨테이너를 빌드하고, 실행합니다.

```bash
$ docker-compose up -d --build
```

응용프로그램 구성을 오버라이드합니다.

```bash
$ mkdif .config
$ cp Bing.Wallpaper.Service.App/appsettings.json ./config
```

`.config/appsettings.json` 파일을 열고 데이터베이스 연결문자열을 수정합니다.

`App:DestinationPath` 값은 고정된 값을 사용하고, 변경하려면 환경변수 `DESTINATION_PATH`의 값을 사용합니다.
`App:RunAtTime` 값을 실행할 시각으로 변경합니다.
`HH:mm:ss` 형식으로 시각을 지정합니다.
