# How to Create DbContext

- Using Visual Studio PM (Package Manager Console):
    ```
    Scaffold-DbContext -Connection name=BookStoreAppDbConnection Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Data/Models" -ContextDir "Data" -Context "BookStoreDbContext" -DataAnnotations
    ```
- Using dotnet cli:
    ```
    dotnet ef dbcontext scaffold Name=ConnectionStrings:BookStoreAppDbConnection  Microsoft.EntityFrameworkCore.SqlServer --output-dir "Data/Models" --context-dir "Data" --context "BookStoreDbContext" --data-annotations
    ```