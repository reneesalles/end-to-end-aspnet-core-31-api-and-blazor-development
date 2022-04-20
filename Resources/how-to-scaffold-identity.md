# How to Scaffold Identity

- Using _dotnet cli_:
  - First we must have installed the following tools:
    1. _dotnet-ef_
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
    1. _Microsoft.AspNetCore.Identity.EntityFrameworkCore_
        ```
        dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
        ```
  - Finally, we must run the following commands:
    - Add a Migration _"AddedIdentityTables"_ (any name does) inside folder _"Data/Migrations"_
        ```
        dotnet ef migrations add AddedIdentityTables -o "Data/Migrations"
        ```
    - Remove any Model reference that __MUST NOT__ be changed by the Migration inside the generated file: (_20220419234108_AddedIdentityTables.cs_, for example)
      - In my case, the Author and Book models
      - Carefull to not edit the _*.Designer.cs_ or the _*ModelSnapshot.cs_ files
    - Update the Database with the previous Migration
        ```
        dotnet ef database update
        ```
