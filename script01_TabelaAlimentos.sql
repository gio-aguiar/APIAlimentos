IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TB_Alimentos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(200) NOT NULL,
    [Calorias] int NOT NULL,
    [Carboidratos] int NOT NULL,
    [Proteinas] int NOT NULL,
    [Gorduras] int NOT NULL,
    [Fibras] int NOT NULL,
    [Sodio] int NOT NULL,
    [Tipo] int NOT NULL,
    CONSTRAINT [PK_TB_Alimentos] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Calorias', N'Carboidratos', N'Fibras', N'Gorduras', N'Nome', N'Proteinas', N'Sodio', N'Tipo') AND [object_id] = OBJECT_ID(N'[TB_Alimentos]'))
    SET IDENTITY_INSERT [TB_Alimentos] ON;
INSERT INTO [TB_Alimentos] ([Id], [Calorias], [Carboidratos], [Fibras], [Gorduras], [Nome], [Proteinas], [Sodio], [Tipo])
VALUES (1, 56, 15, 1, 0, 'Maçã', 0, 0, 1),
(2, 98, 26, 2, 0, 'Banana', 1, 21, 1),
(3, 207, 1, 0, 11, 'Picanha', 20, 450, 4);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Calorias', N'Carboidratos', N'Fibras', N'Gorduras', N'Nome', N'Proteinas', N'Sodio', N'Tipo') AND [object_id] = OBJECT_ID(N'[TB_Alimentos]'))
    SET IDENTITY_INSERT [TB_Alimentos] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240430130616_InitialCreate', N'8.0.4');
GO

COMMIT;
GO

