Build started...
Build succeeded.
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

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categorias] (
    [CategoriaId] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([CategoriaId])
);
GO

CREATE TABLE [Mesas] (
    [MesaId] int NOT NULL IDENTITY,
    [Numero] nvarchar(20) NOT NULL,
    [Ocupada] bit NOT NULL,
    CONSTRAINT [PK_Mesas] PRIMARY KEY ([MesaId])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Productos] (
    [ProductoId] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NOT NULL,
    [Precio] decimal(18,2) NOT NULL,
    [Codigo] nvarchar(max) NOT NULL,
    [CategoriaId] int NOT NULL,
    CONSTRAINT [PK_Productos] PRIMARY KEY ([ProductoId]),
    CONSTRAINT [FK_Productos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([CategoriaId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Ordenes] (
    [OrdenId] int NOT NULL IDENTITY,
    [MesaId] int NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaCierre] datetime2 NULL,
    [Estado] int NOT NULL,
    [Subtotal] decimal(18,2) NOT NULL,
    [Propina] decimal(18,2) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Ordenes] PRIMARY KEY ([OrdenId]),
    CONSTRAINT [FK_Ordenes_Mesas_MesaId] FOREIGN KEY ([MesaId]) REFERENCES [Mesas] ([MesaId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [DetalleOrdenes] (
    [DetalleOrdenId] int NOT NULL IDENTITY,
    [OrdenId] int NOT NULL,
    [ProductoId] int NOT NULL,
    [Cantidad] int NOT NULL,
    [PrecioUnitario] decimal(18,2) NOT NULL,
    [Subtotal] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_DetalleOrdenes] PRIMARY KEY ([DetalleOrdenId]),
    CONSTRAINT [FK_DetalleOrdenes_Ordenes_OrdenId] FOREIGN KEY ([OrdenId]) REFERENCES [Ordenes] ([OrdenId]) ON DELETE CASCADE,
    CONSTRAINT [FK_DetalleOrdenes_Productos_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [Productos] ([ProductoId]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoriaId', N'Nombre') AND [object_id] = OBJECT_ID(N'[Categorias]'))
    SET IDENTITY_INSERT [Categorias] ON;
INSERT INTO [Categorias] ([CategoriaId], [Nombre])
VALUES (1, N'Platos Fuertes'),
(2, N'Bebidas'),
(3, N'Cerveza'),
(4, N'Licor'),
(5, N'Extras');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoriaId', N'Nombre') AND [object_id] = OBJECT_ID(N'[Categorias]'))
    SET IDENTITY_INSERT [Categorias] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MesaId', N'Numero', N'Ocupada') AND [object_id] = OBJECT_ID(N'[Mesas]'))
    SET IDENTITY_INSERT [Mesas] ON;
INSERT INTO [Mesas] ([MesaId], [Numero], [Ocupada])
VALUES (1, N'1', CAST(0 AS bit)),
(2, N'2', CAST(0 AS bit)),
(3, N'3', CAST(0 AS bit)),
(4, N'4', CAST(0 AS bit)),
(5, N'5', CAST(0 AS bit)),
(6, N'6', CAST(0 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MesaId', N'Numero', N'Ocupada') AND [object_id] = OBJECT_ID(N'[Mesas]'))
    SET IDENTITY_INSERT [Mesas] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductoId', N'CategoriaId', N'Codigo', N'Nombre', N'Precio') AND [object_id] = OBJECT_ID(N'[Productos]'))
    SET IDENTITY_INSERT [Productos] ON;
INSERT INTO [Productos] ([ProductoId], [CategoriaId], [Codigo], [Nombre], [Precio])
VALUES (1, 1, N'T1', N'Tacos (3) Pastor', 30.0),
(2, 1, N'T2', N'Tacos (3) Birria', 30.0),
(3, 1, N'T3', N'Tacos (3) Pollo', 30.0),
(4, 1, N'T4', N'Tacos (3) Res', 30.0),
(5, 1, N'T1Q', N'Tacos (3) Queso-Pastor', 45.0),
(6, 1, N'T2Q', N'Tacos (3) Queso-Birria', 45.0),
(7, 1, N'T3Q', N'Tacos (3) Queso-Pollo', 45.0),
(8, 1, N'T4Q', N'Tacos (3) Queso-Res', 45.0),
(9, 1, N'G1', N'Gringa', 35.0),
(10, 1, N'N1', N'Nachos de la casa', 30.0),
(11, 1, N'S1', N'Sopa de Birria', 25.0),
(12, 1, N'N2', N'Cevinachos', 35.0),
(13, 1, N'C1', N'Ceviche Tradicional', 70.0),
(14, 1, N'C2', N'Ceviche Peruano', 70.0),
(15, 1, N'C3', N'Ceviche Aguachile', 70.0),
(16, 1, N'Ca1', N'Camarones al Ajillo', 70.0),
(17, 2, N'b1', N'Jamaica', 15.0),
(18, 2, N'b2', N'Crema', 15.0),
(19, 2, N'b3', N'Horchata', 15.0),
(20, 2, N'b4', N'Tamarindo', 15.0),
(21, 2, N'b5', N'Limonada', 15.0),
(22, 2, N'b6', N'Sodas', 10.0),
(23, 2, N'b7', N'Mineral preparada', 20.0),
(24, 2, N'b8', N'Mineral con jugo', 25.0),
(25, 2, N'b9', N'Agua Pura', 5.0),
(26, 2, N'b10', N'Cafe', 10.0),
(27, 3, N'cer-1', N'Gallo', 15.0),
(28, 3, N'cer-2', N'Corona', 20.0),
(29, 3, N'cer-3', N'Cubetazo Gallo', 75.0),
(30, 3, N'cer-4', N'Cubetazo Corona', 100.0),
(31, 3, N'cer-5', N'Michelada', 35.0),
(32, 3, N'cer-6', N'Picona Gallo', 18.0),
(33, 3, N'cer-7', N'Picona Tecate', 15.0),
(34, 4, N'lic-1', N'Suerito', 25.0),
(35, 4, N'lic-2', N'Botella Venado Light', 170.0),
(36, 4, N'lic-3', N'1/2 Venado Light o XL', 85.0),
(37, 4, N'lic-4', N'Quetzalteca preparada', 25.0),
(38, 4, N'lic-5', N'Botella old parr', 525.0),
(39, 4, N'lic-6', N'Botella Buchanan''s', 550.0),
(40, 4, N'lic-7', N'Red Label', 250.0),
(41, 4, N'lic-8', N'Tequila Jose Cuervo', 250.0),
(42, 4, N'lic-9', N'Tequila Don Julio 70', 750.0);
INSERT INTO [Productos] ([ProductoId], [CategoriaId], [Codigo], [Nombre], [Precio])
VALUES (43, 4, N'lic-10', N'Botella Jagermeister', 500.0),
(44, 5, N'ex-1', N'Helado', 25.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductoId', N'CategoriaId', N'Codigo', N'Nombre', N'Precio') AND [object_id] = OBJECT_ID(N'[Productos]'))
    SET IDENTITY_INSERT [Productos] OFF;
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_DetalleOrdenes_OrdenId] ON [DetalleOrdenes] ([OrdenId]);
GO

CREATE INDEX [IX_DetalleOrdenes_ProductoId] ON [DetalleOrdenes] ([ProductoId]);
GO

CREATE INDEX [IX_Ordenes_MesaId] ON [Ordenes] ([MesaId]);
GO

CREATE INDEX [IX_Productos_CategoriaId] ON [Productos] ([CategoriaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250320195048_InitialModel', N'8.0.14');
GO

COMMIT;
GO


