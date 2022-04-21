# How to Manage User Secrets

- Using _dotnet cli_:
  - First we must check if the _user-secrets_ command exists:
    ```
    dotnet user-secrets list
    ```
    - If the output says it doesn't exists, we must run the _init_ command (on the same folder as the .csproj file):
        ```
        dotnet user-secrets init
        ```
  - Then we can do the followings:
    1. add a new User Secret with the _set_ command:
        ```
        dotnet user-secrets set "JwtSettings:Key" "123456"
        ```
    2. remove a User Secret with the _remove_ command:
        ```
        dotnet user-secrets remove "JwtSettings:Key"
        ```
    3. clear all User Secrets with the _clear_ command:
        ```
        dotnet user-secrets clear
        ```
    4. list all User Secrets with the _list_ command:
        ```
        dotnet user-secrets list
        ```