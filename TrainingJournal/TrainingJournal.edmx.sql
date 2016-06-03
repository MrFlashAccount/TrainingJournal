
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/03/2016 23:48:59
-- Generated from EDMX file: D:\Documents\Visual Studio 2015\Projects\TrainingJournal\TrainingJournal\TrainingJournal.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TrainJournal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_1RPmax_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[1RPmax] DROP CONSTRAINT [FK_1RPmax_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ArmTable_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArmTable] DROP CONSTRAINT [FK_ArmTable_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ChestTable_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChestTable] DROP CONSTRAINT [FK_ChestTable_User];
GO
IF OBJECT_ID(N'[dbo].[FK_HipTable_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HipTable] DROP CONSTRAINT [FK_HipTable_User];
GO
IF OBJECT_ID(N'[dbo].[FK_NeckTable_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NeckTable] DROP CONSTRAINT [FK_NeckTable_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ShinTable_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShinTable] DROP CONSTRAINT [FK_ShinTable_User];
GO
IF OBJECT_ID(N'[dbo].[FK_TrainJournal_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainJournal] DROP CONSTRAINT [FK_TrainJournal_User];
GO
IF OBJECT_ID(N'[dbo].[FK_WaistTable_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WaistTable] DROP CONSTRAINT [FK_WaistTable_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Weight_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Weight] DROP CONSTRAINT [FK_Weight_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[1RPmax]', 'U') IS NOT NULL
    DROP TABLE [dbo].[1RPmax];
GO
IF OBJECT_ID(N'[dbo].[ArmTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArmTable];
GO
IF OBJECT_ID(N'[dbo].[ChestTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChestTable];
GO
IF OBJECT_ID(N'[dbo].[HipTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HipTable];
GO
IF OBJECT_ID(N'[dbo].[NeckTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NeckTable];
GO
IF OBJECT_ID(N'[dbo].[ShinTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShinTable];
GO
IF OBJECT_ID(N'[dbo].[TrainJournal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainJournal];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[WaistTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WaistTable];
GO
IF OBJECT_ID(N'[dbo].[Weight]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Weight];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C1RPmax'
CREATE TABLE [dbo].[C1RPmax] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [BenchPress] real  NOT NULL,
    [Squat] real  NOT NULL,
    [Deadlift] real  NOT NULL
);
GO

-- Creating table 'ArmTables'
CREATE TABLE [dbo].[ArmTables] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Arm] real  NOT NULL,
    [Login] varchar(50)  NOT NULL
);
GO

-- Creating table 'ChestTables'
CREATE TABLE [dbo].[ChestTables] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Chest] real  NOT NULL,
    [Login] varchar(50)  NOT NULL
);
GO

-- Creating table 'HipTables'
CREATE TABLE [dbo].[HipTables] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Hip] real  NOT NULL,
    [Login] varchar(50)  NOT NULL
);
GO

-- Creating table 'NeckTables'
CREATE TABLE [dbo].[NeckTables] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Neck] real  NOT NULL,
    [Login] varchar(50)  NOT NULL
);
GO

-- Creating table 'ShinTables'
CREATE TABLE [dbo].[ShinTables] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Shin] real  NOT NULL,
    [Login] varchar(50)  NOT NULL
);
GO

-- Creating table 'TrainJournals'
CREATE TABLE [dbo].[TrainJournals] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [NumOfSets] int  NOT NULL,
    [NumOfReps] int  NOT NULL,
    [Weight] real  NOT NULL,
    [Comment] nvarchar(100)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Identificator] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Image] varchar(50)  NOT NULL
);
GO

-- Creating table 'WaistTables'
CREATE TABLE [dbo].[WaistTables] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Waist] real  NOT NULL,
    [Login] varchar(50)  NOT NULL
);
GO

-- Creating table 'Weights'
CREATE TABLE [dbo].[Weights] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Weight1] real  NOT NULL,
    [FatPercent] int  NULL,
    [Date] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Identificator] in table 'C1RPmax'
ALTER TABLE [dbo].[C1RPmax]
ADD CONSTRAINT [PK_C1RPmax]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'ArmTables'
ALTER TABLE [dbo].[ArmTables]
ADD CONSTRAINT [PK_ArmTables]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'ChestTables'
ALTER TABLE [dbo].[ChestTables]
ADD CONSTRAINT [PK_ChestTables]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'HipTables'
ALTER TABLE [dbo].[HipTables]
ADD CONSTRAINT [PK_HipTables]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'NeckTables'
ALTER TABLE [dbo].[NeckTables]
ADD CONSTRAINT [PK_NeckTables]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'ShinTables'
ALTER TABLE [dbo].[ShinTables]
ADD CONSTRAINT [PK_ShinTables]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'TrainJournals'
ALTER TABLE [dbo].[TrainJournals]
ADD CONSTRAINT [PK_TrainJournals]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'WaistTables'
ALTER TABLE [dbo].[WaistTables]
ADD CONSTRAINT [PK_WaistTables]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'Weights'
ALTER TABLE [dbo].[Weights]
ADD CONSTRAINT [PK_Weights]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Login] in table 'C1RPmax'
ALTER TABLE [dbo].[C1RPmax]
ADD CONSTRAINT [FK_1RPmax_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_1RPmax_User'
CREATE INDEX [IX_FK_1RPmax_User]
ON [dbo].[C1RPmax]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'ArmTables'
ALTER TABLE [dbo].[ArmTables]
ADD CONSTRAINT [FK_ArmTable_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArmTable_User'
CREATE INDEX [IX_FK_ArmTable_User]
ON [dbo].[ArmTables]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'ChestTables'
ALTER TABLE [dbo].[ChestTables]
ADD CONSTRAINT [FK_ChestTable_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChestTable_User'
CREATE INDEX [IX_FK_ChestTable_User]
ON [dbo].[ChestTables]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'HipTables'
ALTER TABLE [dbo].[HipTables]
ADD CONSTRAINT [FK_HipTable_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HipTable_User'
CREATE INDEX [IX_FK_HipTable_User]
ON [dbo].[HipTables]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'NeckTables'
ALTER TABLE [dbo].[NeckTables]
ADD CONSTRAINT [FK_NeckTable_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NeckTable_User'
CREATE INDEX [IX_FK_NeckTable_User]
ON [dbo].[NeckTables]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'ShinTables'
ALTER TABLE [dbo].[ShinTables]
ADD CONSTRAINT [FK_ShinTable_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShinTable_User'
CREATE INDEX [IX_FK_ShinTable_User]
ON [dbo].[ShinTables]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'TrainJournals'
ALTER TABLE [dbo].[TrainJournals]
ADD CONSTRAINT [FK_TrainJournal_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrainJournal_User'
CREATE INDEX [IX_FK_TrainJournal_User]
ON [dbo].[TrainJournals]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'WaistTables'
ALTER TABLE [dbo].[WaistTables]
ADD CONSTRAINT [FK_WaistTable_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WaistTable_User'
CREATE INDEX [IX_FK_WaistTable_User]
ON [dbo].[WaistTables]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'Weights'
ALTER TABLE [dbo].[Weights]
ADD CONSTRAINT [FK_Weight_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[Users]
        ([Identificator])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Weight_User'
CREATE INDEX [IX_FK_Weight_User]
ON [dbo].[Weights]
    ([Login]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------