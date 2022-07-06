
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/04/2022 22:59:04
-- Generated from EDMX file: C:\Users\Matthew Herman\source\repos\NewApplication\NewApplication\DBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db915559979];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActivitySession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_ActivitySession];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityZone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_ActivityZone];
GO
IF OBJECT_ID(N'[dbo].[FK_AgeGroupActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_AgeGroupActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_ClubParticipant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participants] DROP CONSTRAINT [FK_ClubParticipant];
GO
IF OBJECT_ID(N'[dbo].[FK_ParticipantAgeGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participants] DROP CONSTRAINT [FK_ParticipantAgeGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_ParticipantSession_Participant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParticipantSession] DROP CONSTRAINT [FK_ParticipantSession_Participant];
GO
IF OBJECT_ID(N'[dbo].[FK_ParticipantSession_Session]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParticipantSession] DROP CONSTRAINT [FK_ParticipantSession_Session];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionGroupSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_SessionGroupSession];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activities];
GO
IF OBJECT_ID(N'[dbo].[AgeGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AgeGroups];
GO
IF OBJECT_ID(N'[dbo].[Clubs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clubs];
GO
IF OBJECT_ID(N'[dbo].[Participants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Participants];
GO
IF OBJECT_ID(N'[dbo].[ParticipantSession]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParticipantSession];
GO
IF OBJECT_ID(N'[dbo].[SessionGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionGroups];
GO
IF OBJECT_ID(N'[dbo].[Sessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sessions];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Zones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zones];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [ActivityId] int IDENTITY(1,1) NOT NULL,
    [AgeGroupAgeGroupId] int  NOT NULL,
    [ActivityName] nvarchar(max)  NOT NULL,
    [Zone_ZoneId] int  NOT NULL
);
GO

-- Creating table 'AgeGroups'
CREATE TABLE [dbo].[AgeGroups] (
    [AgeGroupId] int IDENTITY(1,1) NOT NULL,
    [AgeGroupName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clubs'
CREATE TABLE [dbo].[Clubs] (
    [ClubId] int IDENTITY(1,1) NOT NULL,
    [ClubName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Participants'
CREATE TABLE [dbo].[Participants] (
    [ParticipantId] int IDENTITY(1,1) NOT NULL,
    [ClubClubId] int  NOT NULL,
    [ParticipantName] nvarchar(max)  NOT NULL,
    [AgeGroup_AgeGroupId] int  NOT NULL
);
GO

-- Creating table 'SessionGroups'
CREATE TABLE [dbo].[SessionGroups] (
    [SessionGroupId] int IDENTITY(1,1) NOT NULL,
    [SessionName] nvarchar(max)  NOT NULL,
    [SessionTime] datetime  NULL
);
GO

-- Creating table 'Sessions'
CREATE TABLE [dbo].[Sessions] (
    [SessionId] int IDENTITY(1,1) NOT NULL,
    [SessionGroupSessionGroupId] int  NOT NULL,
    [ActivityActivityId] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Zones'
CREATE TABLE [dbo].[Zones] (
    [ZoneId] int IDENTITY(1,1) NOT NULL,
    [ZoneName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ParticipantSession'
CREATE TABLE [dbo].[ParticipantSession] (
    [Participants_ParticipantId] int  NOT NULL,
    [Sessions_SessionId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ActivityId] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([ActivityId] ASC);
GO

-- Creating primary key on [AgeGroupId] in table 'AgeGroups'
ALTER TABLE [dbo].[AgeGroups]
ADD CONSTRAINT [PK_AgeGroups]
    PRIMARY KEY CLUSTERED ([AgeGroupId] ASC);
GO

-- Creating primary key on [ClubId] in table 'Clubs'
ALTER TABLE [dbo].[Clubs]
ADD CONSTRAINT [PK_Clubs]
    PRIMARY KEY CLUSTERED ([ClubId] ASC);
GO

-- Creating primary key on [ParticipantId] in table 'Participants'
ALTER TABLE [dbo].[Participants]
ADD CONSTRAINT [PK_Participants]
    PRIMARY KEY CLUSTERED ([ParticipantId] ASC);
GO

-- Creating primary key on [SessionGroupId] in table 'SessionGroups'
ALTER TABLE [dbo].[SessionGroups]
ADD CONSTRAINT [PK_SessionGroups]
    PRIMARY KEY CLUSTERED ([SessionGroupId] ASC);
GO

-- Creating primary key on [SessionId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [PK_Sessions]
    PRIMARY KEY CLUSTERED ([SessionId] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ZoneId] in table 'Zones'
ALTER TABLE [dbo].[Zones]
ADD CONSTRAINT [PK_Zones]
    PRIMARY KEY CLUSTERED ([ZoneId] ASC);
GO

-- Creating primary key on [Participants_ParticipantId], [Sessions_SessionId] in table 'ParticipantSession'
ALTER TABLE [dbo].[ParticipantSession]
ADD CONSTRAINT [PK_ParticipantSession]
    PRIMARY KEY CLUSTERED ([Participants_ParticipantId], [Sessions_SessionId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActivityActivityId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_ActivitySession]
    FOREIGN KEY ([ActivityActivityId])
    REFERENCES [dbo].[Activities]
        ([ActivityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivitySession'
CREATE INDEX [IX_FK_ActivitySession]
ON [dbo].[Sessions]
    ([ActivityActivityId]);
GO

-- Creating foreign key on [Zone_ZoneId] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_ActivityZone]
    FOREIGN KEY ([Zone_ZoneId])
    REFERENCES [dbo].[Zones]
        ([ZoneId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityZone'
CREATE INDEX [IX_FK_ActivityZone]
ON [dbo].[Activities]
    ([Zone_ZoneId]);
GO

-- Creating foreign key on [AgeGroupAgeGroupId] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_AgeGroupActivity]
    FOREIGN KEY ([AgeGroupAgeGroupId])
    REFERENCES [dbo].[AgeGroups]
        ([AgeGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AgeGroupActivity'
CREATE INDEX [IX_FK_AgeGroupActivity]
ON [dbo].[Activities]
    ([AgeGroupAgeGroupId]);
GO

-- Creating foreign key on [AgeGroup_AgeGroupId] in table 'Participants'
ALTER TABLE [dbo].[Participants]
ADD CONSTRAINT [FK_ParticipantAgeGroup]
    FOREIGN KEY ([AgeGroup_AgeGroupId])
    REFERENCES [dbo].[AgeGroups]
        ([AgeGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ParticipantAgeGroup'
CREATE INDEX [IX_FK_ParticipantAgeGroup]
ON [dbo].[Participants]
    ([AgeGroup_AgeGroupId]);
GO

-- Creating foreign key on [ClubClubId] in table 'Participants'
ALTER TABLE [dbo].[Participants]
ADD CONSTRAINT [FK_ClubParticipant]
    FOREIGN KEY ([ClubClubId])
    REFERENCES [dbo].[Clubs]
        ([ClubId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClubParticipant'
CREATE INDEX [IX_FK_ClubParticipant]
ON [dbo].[Participants]
    ([ClubClubId]);
GO

-- Creating foreign key on [SessionGroupSessionGroupId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionGroupSession]
    FOREIGN KEY ([SessionGroupSessionGroupId])
    REFERENCES [dbo].[SessionGroups]
        ([SessionGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionGroupSession'
CREATE INDEX [IX_FK_SessionGroupSession]
ON [dbo].[Sessions]
    ([SessionGroupSessionGroupId]);
GO

-- Creating foreign key on [Participants_ParticipantId] in table 'ParticipantSession'
ALTER TABLE [dbo].[ParticipantSession]
ADD CONSTRAINT [FK_ParticipantSession_Participant]
    FOREIGN KEY ([Participants_ParticipantId])
    REFERENCES [dbo].[Participants]
        ([ParticipantId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Sessions_SessionId] in table 'ParticipantSession'
ALTER TABLE [dbo].[ParticipantSession]
ADD CONSTRAINT [FK_ParticipantSession_Session]
    FOREIGN KEY ([Sessions_SessionId])
    REFERENCES [dbo].[Sessions]
        ([SessionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ParticipantSession_Session'
CREATE INDEX [IX_FK_ParticipantSession_Session]
ON [dbo].[ParticipantSession]
    ([Sessions_SessionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------