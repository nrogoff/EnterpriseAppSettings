CREATE TABLE [dbo].[AppSettingGroup]
(
	[AppSettingGroupId] INT NOT NULL PRIMARY KEY IDENTITY,
    [ParentGroupId] INT NULL, 
	[Group] VARCHAR(50) NOT NULL,
	[Description] NVARCHAR(1000),
	[GroupPath] AS ([dbo].[GetGroupPath]([AppSettingGroupId])),	
	[ModifiedDate] DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL, 
    CONSTRAINT [FK_AppSettingGroup_AppSettingGroup] FOREIGN KEY ([ParentGroupId]) REFERENCES [AppSettingGroup]([AppSettingGroupId])
)
GO

CREATE TRIGGER [dbo].[Trigger_AppSettingGroup_Audit]
    ON [dbo].[AppSettingGroup]
    FOR DELETE, UPDATE
    AS
    BEGIN
        SET NoCount ON;
		INSERT INTO dbo.AppSettingGroup_Audit ([AppSettingGroupId],[ParentGroupId],[Group],[Description],[ModifiedDate], [ModifiedBy])
		SELECT [AppSettingGroupId],[ParentGroupId],[Group],[Description],[ModifiedDate], [ModifiedBy] FROM deleted
    END


