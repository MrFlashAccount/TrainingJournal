
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/11/2016 11:17:02
-- Generated from EDMX file: D:\Documents\Visual Studio 2015\Projects\TrainingJournal\TrainingJournal\TrainJournal.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Training Journal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TrainJournal_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainJournal] DROP CONSTRAINT [FK_TrainJournal_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAntropometry_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAntropometry] DROP CONSTRAINT [FK_UserAntropometry_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Weight_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Weight] DROP CONSTRAINT [FK_Weight_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TrainJournal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainJournal];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserAntropometry]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAntropometry];
GO
IF OBJECT_ID(N'[dbo].[Weight]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Weight];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TrainJournal'
CREATE TABLE [dbo].[TrainJournal] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [NumOfSets] tinyint  NOT NULL,
    [NumOfReps] tinyint  NOT NULL,
    [Comment] nvarchar(100)  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Identificator] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Image] varchar(50)  NOT NULL
);
GO

-- Creating table 'UserAntropometry'
CREATE TABLE [dbo].[UserAntropometry] (
    [Identificator] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Nech] real  NULL,
    [Chest] real  NULL,
    [Arm] real  NULL,
    [Waist] real  NULL,
    [Hip] real  NULL,
    [Shin] real  NULL,
    [Date] datetime  NULL
);
GO

-- Creating table 'Weight'
CREATE TABLE [dbo].[Weight] (
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

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Identificator] in table 'TrainJournal'
ALTER TABLE [dbo].[TrainJournal]
ADD CONSTRAINT [PK_TrainJournal]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'UserAntropometry'
ALTER TABLE [dbo].[UserAntropometry]
ADD CONSTRAINT [PK_UserAntropometry]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- Creating primary key on [Identificator] in table 'Weight'
ALTER TABLE [dbo].[Weight]
ADD CONSTRAINT [PK_Weight]
    PRIMARY KEY CLUSTERED ([Identificator] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Login] in table 'TrainJournal'
ALTER TABLE [dbo].[TrainJournal]
ADD CONSTRAINT [FK_TrainJournal_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[User]
        ([Identificator])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrainJournal_User'
CREATE INDEX [IX_FK_TrainJournal_User]
ON [dbo].[TrainJournal]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'UserAntropometry'
ALTER TABLE [dbo].[UserAntropometry]
ADD CONSTRAINT [FK_UserAntropometry_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[User]
        ([Identificator])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAntropometry_User'
CREATE INDEX [IX_FK_UserAntropometry_User]
ON [dbo].[UserAntropometry]
    ([Login]);
GO

-- Creating foreign key on [Login] in table 'Weight'
ALTER TABLE [dbo].[Weight]
ADD CONSTRAINT [FK_Weight_User]
    FOREIGN KEY ([Login])
    REFERENCES [dbo].[User]
        ([Identificator])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Weight_User'
CREATE INDEX [IX_FK_Weight_User]
ON [dbo].[Weight]
    ([Login]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------