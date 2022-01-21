---
title: "Bing Today Image Collector"
---

[![Build and tag](https://github.com/bbonkr/bing-wallpaper/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bbonkr/bing-wallpaper/actions/workflows/dotnet.yml) [![docker image](https://github.com/bbonkr/bing-wallpaper/actions/workflows/docker.yml/badge.svg)](https://github.com/bbonkr/bing-wallpaper/actions/workflows/docker.yml)

# Bing Today Image Collector

Bing.com 오늘의 이미지를 수집합니다.

⚠ 수집된 이미지 파일은 저작권을 주의해야 합니다.

저는 `Windows`, `MacOS` **배경화면**으로만 사용합니다.

## 준비

EF Core 3 부터 전역 또는 로컬 도구로 설치되어야 합니다.

### EF 도구 설치

```bash
$ dotnet tool install --global dotnet-ef
```

```bash
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

UPDATE 2020-11-14: .NET 5 사용

이전 EF 도구가 설치되어 있는 경우 아래 명령으로 .NET 5 EF 도구로 업데이트할 수 있습니다.

```bash
$ dotnet tool update --global dotnet-ef
```

업데이트된 EF 도구의 버전을 확인합니다.

```bash
$ dotnet ef

                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 5.0.0

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

### 데이터베이스 마이그레이션 코드 작성

```powershell
PS> ./ef-migration-add.ps1 "Add some fields"
```

```bash
$ ./ef-migration-add.sh "Add some fields"
```

> ef-migration-add.sh 파일을 사용하기 위해서는 실행 권한이 있어야 합니다.
> ```bash
> $ chmod u+x ./ef-migration-add.sh
> ```


## 실행

### 로컬에서 실행

원격 저장소의 파일을 로컬 저장소로 복제합니다.

```bash
$ cd Bing.Wallpaper
$ dotnet run
```

### 도커 컨테이너로 실행

원격 저장소의 파일을 로컬 저장소로 복제합니다.

docker-compose.webapp.yml 파일을 docker-compose.yml 로 복사합니다.

```bash
$ cp docker-compose.webapp.yml docker-compose.yml
```

`docker-compose.yml` 파일을 열고 아래 구성을 변경합니다.

`environment` 목록에서 환경 변수를 설정합니다.

* ConnectionStrings__Default: 데이터베이스 연결 문자열을 설정합니다.
* Collector__Schedule: 이미지 수집 일정을 설정합니다.

`volumes` 목록에서 이미지 파일을 저장할 경로를 `/app/images` 디렉터리에 마운트합니다. 

예) `/docker/bing-images-app/images` 경로에 파일이미지를 저장하려는 경우 아래와 같이 입력합니다.

```yaml
# 생략
    volumes:
      - "/docker/bing-images-app/images:/app/images"
```

도커 컴포즈를 사용해서 컨테이너를 빌드하고, 컨테이너를 실행합니다.

```bash
$ docker-compose up -d --build
```

