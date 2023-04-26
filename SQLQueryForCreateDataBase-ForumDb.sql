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

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [RoleName] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(32) NOT NULL,
    [LastName] nvarchar(32) NOT NULL,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [IsUserBlocked] bit NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Posts] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(64) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [Likes] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(max) NULL,
    [PostId] int NOT NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([Id], [RoleName])
VALUES (1, N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([Id], [RoleName])
VALUES (2, N'user');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'IsUserBlocked', N'LastName', N'Password', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [Email], [FirstName], [IsUserBlocked], [LastName], [Password], [PhoneNumber], [RoleId], [Username])
VALUES (1, N'vpatova@abv.bg', N'Valentina', CAST(0 AS bit), N'Patova', N'123', N'555-555-5555', 1, N'valia');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'IsUserBlocked', N'LastName', N'Password', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'IsUserBlocked', N'LastName', N'Password', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [Email], [FirstName], [IsUserBlocked], [LastName], [Password], [PhoneNumber], [RoleId], [Username])
VALUES (3, N'vi@abv.bg', N'Valentina', CAST(0 AS bit), N'Iordanova', N'123', N'777-777-7777', 2, N'valia2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'IsUserBlocked', N'LastName', N'Password', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'IsUserBlocked', N'LastName', N'Password', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [Email], [FirstName], [IsUserBlocked], [LastName], [Password], [PhoneNumber], [RoleId], [Username])
VALUES (2, N'mivanov@abv.bg', N'Miroslav', CAST(0 AS bit), N'Ivanov', N'123', N'666-666-6666', 2, N'miro');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'IsUserBlocked', N'LastName', N'Password', N'PhoneNumber', N'RoleId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'CreatedOn', N'Likes', N'Title', N'UserId') AND [object_id] = OBJECT_ID(N'[Posts]'))
    SET IDENTITY_INSERT [Posts] ON;
INSERT INTO [Posts] ([Id], [Content], [CreatedOn], [Likes], [Title], [UserId])
VALUES (2, N'Great Time at the beach !', '2022-05-21T19:23:57.7842743+03:00', 1, N'We went to the Golden Sands', 1),
(6, N'Conted Of Sixth Post', '2022-05-21T19:23:57.7842779+03:00', 1, N'Sixth Post', 1),
(7, N'Conted Of Seventh Post', '2022-05-21T19:23:57.7842782+03:00', 1, N'Seventh Post', 1),
(11, N'Conted Of Eleventh Post', '2022-05-21T19:23:57.7842793+03:00', 1, N'Eleventh Post', 1),
(3, N'Went skiing to Lake Tahoe last week ,we had a great time', '2022-05-21T19:23:57.7842767+03:00', 1, N'Lake Tahoe visit', 3),
(5, N'Conted Of Fifth Post', '2022-05-21T19:23:57.7842775+03:00', 1, N'Fifth Post', 3),
(9, N'Conted Of Ninth Post', '2022-05-21T19:23:57.7842787+03:00', 1, N'Ninth Post', 3),
(1, N'Our Vacation was amazing .. ', '2022-05-21T19:23:57.7825922+03:00', 3, N'Vacation in Sunny beach', 2),
(4, N'Super Nice place to stay', '2022-05-21T19:23:57.7842772+03:00', 1, N'Grand Pyramids Hotel', 2),
(8, N'Conted Of Eight Post', '2022-05-21T19:23:57.7842785+03:00', 1, N'Eight Post', 2),
(10, N'Conted Of Tenth Post', '2022-05-21T19:23:57.7842791+03:00', 1, N'Tenth Post', 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'CreatedOn', N'Likes', N'Title', N'UserId') AND [object_id] = OBJECT_ID(N'[Posts]'))
    SET IDENTITY_INSERT [Posts] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'PostId', N'UserId') AND [object_id] = OBJECT_ID(N'[Comments]'))
    SET IDENTITY_INSERT [Comments] ON;
INSERT INTO [Comments] ([Id], [Content], [PostId], [UserId])
VALUES (4, N'4 Comment', 2, 2),
(5, N'5 Comment', 2, 1),
(6, N'6 Comment', 2, 3),
(7, N'7 Comment', 3, 2),
(8, N'8 Comment', 3, 3),
(10, N'10 Comment', 5, 3),
(11, N'11 Comment', 5, 3),
(12, N'12 Comment', 5, 2),
(13, N'13 Comment', 5, 2),
(14, N'14 Comment', 5, 1),
(1, N'1 Comment', 1, 2),
(2, N'2 Comment', 1, 1),
(3, N'3 Comment', 1, 3),
(9, N'9 Comment', 4, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'PostId', N'UserId') AND [object_id] = OBJECT_ID(N'[Comments]'))
    SET IDENTITY_INSERT [Comments] OFF;
GO

CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
GO

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220521162358_Initial', N'5.0.16');
GO

COMMIT;
GO

