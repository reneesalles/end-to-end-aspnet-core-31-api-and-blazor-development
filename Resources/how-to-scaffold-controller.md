# How to Scaffold Controller

- Using _dotnet cli_:
  - First we must have installed the following tools (global):
    1. _dotnet-aspnet-codegenerator_
       - check if it is installed with:
            ```
            dotnet aspnet-codegenerator --help
            ```
       - if it is installed, update with:
            ```
            dotnet tool update --global dotnet-aspnet-codegenerator
            ```
       - if it is not installed, install with:
            ```
            dotnet tool install --global dotnet-aspnet-codegenerator
            ```
    2. _dotnet-ef_
       - check if it is installed with:
            ```
            dotnet ef --help
            ```
       - if it is installed, update with:
            ```
            dotnet tool update --global dotnet-ef
            ```
       - if it is not installed, install with:
            ```
            dotnet tool install --global dotnet-ef
            ```
  - Then we must have installed the following packages (project):
    1. _Microsoft.EntityFrameworkCore.Design_
        ```
        dotnet add package Microsoft.EntityFrameworkCore.Design
        ```
    2. _Microsoft.EntityFrameworkCore.SqlServer_
        ```
        dotnet add package Microsoft.EntityFrameworkCore.SqlServer
        ```
       - If using a database other then SqlServer, we still need it to use the _dotnet-aspnet-codegenerator_
            ```
            dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            dotnet add package Microsoft.EntityFrameworkCore.SQLite
            ```
    3. _Microsoft.VisualStudio.Web.CodeGeneration.Design_
        ```
        dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
        ```
  - Finally, we must run the following commands:
    ```
    dotnet aspnet-codegenerator controller -name AuthorsController -m Author -dc BookStoreDbContext -outDir Controllers -api
    ```
    - Parameters:
      - -name -> Name of the Controller
      - -m -> Model class to use
      - -dc -> DbContext class to use
      - -outDir -> Name of the relative folder to output the Controller
      - -api -> Generate a Controller with REST style API

### Resources:

- [dotnet-aspnet-codegenerator (Docs Microsoft)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-6.0)
