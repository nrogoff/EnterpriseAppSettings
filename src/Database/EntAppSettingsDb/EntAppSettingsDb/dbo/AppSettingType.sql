CREATE TABLE [dbo].[AppSettingType]
(
	[AppSettingTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AppSettingType] NVARCHAR(50) NOT NULL, 
    [AppSettingTypeDescription] NVARCHAR(200) NULL,
	[ModifiedDate] DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL
)

GO

CREATE UNIQUE INDEX [UX_AppSettingType_AppSettingType] ON [dbo].[AppSettingType] ([AppSettingType])
