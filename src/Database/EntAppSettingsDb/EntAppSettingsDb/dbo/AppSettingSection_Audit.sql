CREATE TABLE [dbo].[AppSettingSection_Audit]
(
	[AuditId] BIGINT IDENTITY (1, 1) NOT NULL,
	[AppSettingSectionId] INT NOT NULL,
    [ParentSectionId] INT NULL, 
	[Section] VARCHAR(50) NOT NULL,
	[Description] NVARCHAR(1000),
	[ModifiedDate] DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL, 
    CONSTRAINT [PK_AppSettingSection_Audit] PRIMARY KEY ([AuditId]),    
)
GO


