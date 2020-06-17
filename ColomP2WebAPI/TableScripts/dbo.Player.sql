CREATE TABLE [dbo].[Player] (
    [Id]        NVARCHAR (128) NOT NULL,
    [name]      NVARCHAR (128) NULL,
    [DateBirth] NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Player_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

