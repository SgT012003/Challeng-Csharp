-- Script SQL Server DDL para criação do banco de dados CarePlusFitDB
-- Gerado a partir dos modelos C# (Code First)

-- 1. Tabela Usuarios
CREATE TABLE [Usuarios] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(100) NOT NULL,
    [Email] nvarchar(150) NOT NULL,
    [PasswordHash] nvarchar(255) NOT NULL,
    [AvatarUrl] nvarchar(255) NULL,
    [Pontos] int NOT NULL,
    [StepsToday] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

-- Índice Único para Email
CREATE UNIQUE INDEX [IX_Usuarios_Email] ON [Usuarios] ([Email]);
GO

-- 2. Tabela Challenges
CREATE TABLE [Challenges] (
    [Id] uniqueidentifier NOT NULL,
    [Titulo] nvarchar(150) NOT NULL,
    [Descricao] nvarchar(max) NULL,
    [Icon] nvarchar(255) NULL,
    [RequiredValue] int NOT NULL,
    [RewardPoints] int NOT NULL,
    [Category] nvarchar(50) NOT NULL,
    [IsDaily] bit NOT NULL,
    [DataLimitePontuacao] datetime2 NOT NULL,
    [Status] int NOT NULL, -- Mapeado de ChallengeStatus enum (0=Waiting, 1=Go, 2=Ended, 3=Deleted)
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Challenges] PRIMARY KEY ([Id])
);
GO

-- 3. Tabela UserChallenges
CREATE TABLE [UserChallenges] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ChallengeId] uniqueidentifier NOT NULL,
    [ProgressValue] int NOT NULL,
    [Completed] bit NOT NULL,
    [CompletedAt] datetime2 NULL,
    [EarnedPoints] int NOT NULL,
    [LastUpdate] datetime2 NOT NULL,
    CONSTRAINT [PK_UserChallenges] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserChallenges_Challenges_ChallengeId] FOREIGN KEY ([ChallengeId]) REFERENCES [Challenges] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserChallenges_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_UserChallenges_ChallengeId] ON [UserChallenges] ([ChallengeId]);
GO

CREATE INDEX [IX_UserChallenges_UserId] ON [UserChallenges] ([UserId]);
GO

-- 4. Tabela WearableConnections (Relação 1:1 com Usuarios)
CREATE TABLE [WearableConnections] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Provider] nvarchar(50) NOT NULL,
    [AccessToken] nvarchar(max) NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [ExpiresAt] datetime2 NOT NULL,
    [LastSyncedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_WearableConnections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WearableConnections_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

-- Índice Único para UserId (garante a relação 1:1)
CREATE UNIQUE INDEX [IX_WearableConnections_UserId] ON [WearableConnections] ([UserId]);
GO

-- 5. Tabela StepLogs
CREATE TABLE [StepLogs] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [LogDate] date NOT NULL, -- Mapeado de DateOnly
    [Steps] int NOT NULL,
    [SyncedFrom] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_StepLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StepLogs_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_StepLogs_UserId] ON [StepLogs] ([UserId]);
GO

-- 6. Tabela Rewards (Benefícios)
CREATE TABLE [Rewards] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CostPoints] int NOT NULL,
    [ImageUrl] nvarchar(255) NULL,
    [Category] nvarchar(50) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Rewards] PRIMARY KEY ([Id])
);
GO

-- 7. Tabela RewardClaims (Resgates de Benefícios)
CREATE TABLE [RewardClaims] (
    [Id] uniqueidentifier NOT NULL,
    [RewardId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [PointsSpent] int NOT NULL,
    [ClaimedAt] datetime2 NOT NULL,
    [Status] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_RewardClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RewardClaims_Rewards_RewardId] FOREIGN KEY ([RewardId]) REFERENCES [Rewards] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RewardClaims_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RewardClaims_RewardId] ON [RewardClaims] ([RewardId]);
GO

CREATE INDEX [IX_RewardClaims_UserId] ON [RewardClaims] ([UserId]);
GO

-- 8. Tabela RankingSnapshots
CREATE TABLE [RankingSnapshots] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Position] int NOT NULL,
    [Points] int NOT NULL,
    [SnapshotDate] date NOT NULL, -- Mapeado de DateOnly
    CONSTRAINT [PK_RankingSnapshots] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RankingSnapshots_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RankingSnapshots_UserId] ON [RankingSnapshots] ([UserId]);
GO
