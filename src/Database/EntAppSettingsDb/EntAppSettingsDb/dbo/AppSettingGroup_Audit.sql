CREATE TABLE [dbo].[AppSettingGroup_Audit]
(
    [AuditId] BIGINT IDENTITY (1, 1) NOT NULL,
	[AppSettingGroupId] INT NOT NULL,
    [ParentGroupId] INT NOT NULL, 
	[Group] VARCHAR(50) NOT NULL,
	[Description] NVARCHAR(1000) NOT NULL,
	[ModifiedDate] DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL, 
    CONSTRAINT [PK_AppSettingGroup_Audit] PRIMARY KEY ([AuditId]), 
)
GO


