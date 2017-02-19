CREATE TABLE [dbo].[AppSettingSection]
(
	[AppSettingSectionId] INT NOT NULL PRIMARY KEY IDENTITY,
    [ParentSectionId] INT NULL, 
	[Section] VARCHAR(50) NOT NULL,
	[Description] NVARCHAR(1000),
	[Ordinality] INT NOT NULL DEFAULT 0,
	[ModifiedDate] DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL, 
    CONSTRAINT [FK_AppSettingSection_AppSettingSection] FOREIGN KEY ([ParentSectionId]) REFERENCES [dbo].[AppSettingSection]([AppSettingSectionId]), 
    
)
GO

CREATE TRIGGER [dbo].[Trigger_AppSettingSection_Audit]
    ON [dbo].[AppSettingSection]
    FOR DELETE, UPDATE
    AS
    BEGIN
        SET NoCount ON;
		INSERT INTO dbo.AppSettingSection_Audit ([AppSettingSectionId],[ParentSectionId],[Section],[Description],[ModifiedDate], [ModifiedBy])
		SELECT [AppSettingSectionId],[ParentSectionId],[Section],[Description],[ModifiedDate],[ModifiedBy] FROM deleted
    END



GO

CREATE INDEX [IX_AppSettingSection_Section_Ordinality] ON [dbo].[AppSettingSection] ([Ordinality], [Section])

GO

CREATE UNIQUE INDEX [UX_AppSettingSection_Section_ParentSectionId] ON [dbo].[AppSettingSection] ([Section], [ParentSectionId])
