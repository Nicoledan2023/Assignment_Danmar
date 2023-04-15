dotnet new webapp
dotnet new gitignore

git init

@REM update tools
dotnet tool update --global dotnet-ef
dotnet tool update --global dotnet-aspnet-codegenerator


@REM ** EFCore db engine support **
@REM ** https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli **
@REM ** SQLite **
@REM dotnet add package Microsoft.EntityFrameworkCore.Sqlite
@REM ** MariaDB/MySQL **
dotnet add package Pomelo.EntityFrameworkCore.MySql
@REM ** SQL Server ** *also required to generate ef/model pages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

@REM * Error detection for EFCore migratiomn errors
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
@REM * Design-time support for Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.Design
@REM * Entity Framework Core tools - allows migrations, update, etc.
dotnet add package Microsoft.EntityFrameworkCore.Tools
@REM * logging classes
dotnet add package Microsoft.Extensions.Logging.Debug

@REM * support for aspnet-codegenerator in code
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

@REM * Identity framework support
@REM dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore\
@REM * Identity UI aspnet-codegenerator support
@REM dotnet add package Microsoft.AspNetCore.Identity.UI


git add .
git commit -m "Initial commit"

@REM * open in VS Code
code .