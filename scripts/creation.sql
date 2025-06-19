CREATE TABLE [Race] (
    [Id] int NOT NULL,
    [Name] varchar(100) NOT NULL,
    PRIMARY KEY ([Id])
);


CREATE TABLE [ExperienceLevel] (
    [Id] int NOT NULL,
    [Name] varchar(100) NOT NULL,
    PRIMARY KEY ([Id])
);


CREATE TABLE [Person] (
    [Id] varchar(100) NOT NULL,
    [FirstName] varchar(100) NOT NULL,
    [MiddleName] varchar(100),
    [LastName] varchar(100) NOT NULL,
    [HasBounty] bit NOT NULL,
    PRIMARY KEY ([Id])
);

CREATE TABLE [Adventurer] (
    [Id] int NOT NULL,
    [Nickname] varchar(100) NOT NULL UNIQUE,
    [RaceId] int NOT NULL,
    [ExperienceId] int NOT NULL,
    [PersonId] varchar(100) NOT NULL,
    PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Adventurer_Race] FOREIGN KEY ([RaceId]) REFERENCES [Race]([Id]),
    CONSTRAINT [FK_Adventurer_ExperienceLevel] FOREIGN KEY ([ExperienceId]) REFERENCES [ExperienceLevel]([Id]),
    CONSTRAINT [FK_Adventurer_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id])
);