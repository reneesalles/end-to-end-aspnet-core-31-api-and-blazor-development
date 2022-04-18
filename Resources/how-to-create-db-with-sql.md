# How to Create DB (and Tables) on SQL Server

- With T-SQL
  - First Create the Database:
    ```
    CREATE DATABASE BookStoreDB
    ```
  - Then Create the Tables:
    - Authors:
        ```
        CREATE TABLE [dbo].[Authors]  
        (  
            [Id] INT NOT NULL PRIMARY KEY IDENTITY,  
            [FirstName] NVARCHAR(50) NULL,  
            [LastName] NVARCHAR(50) NULL,  
            [Bio] NVARCHAR(250) NULL  
        )  
        ```
    - Books:
        ```
        CREATE TABLE [dbo].[Books]  
        (  
            [Id] INT NOT NULL PRIMARY KEY   IDENTITY,
            [Title] NVARCHAR(50) NULL,  
            [Year] INT NULL,  
            [ISBN] NVARCHAR(50) NOT NULL   UNIQUE,
            [Summary] NVARCHAR(250) NULL,  
            [Image] NVARCHAR(50) NULL,  
            [Price] DECIMAL(18, 2) NULL,  
            [AuthorId] INT NULL,  
            CONSTRAINT [FK_Books_ToTable]   FOREIGN KEY ([AuthorId]) REFERENCES [Authors]([Id])
        )  
        ```
