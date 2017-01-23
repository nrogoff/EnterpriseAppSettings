CREATE TABLE [dbo].[AppSetting_Audit] (
    [AuditId] BIGINT IDENTITY (1, 1) NOT NULL,
	[AppSettingId] INT NOT NULL, 
	[SettingKey]   NVARCHAR (50)   NOT NULL,
    [TenantId]  INT NOT NULL DEFAULT 0,
    [SettingGroupId] INT   NOT NULL DEFAULT 0,
    [SettingSectionId] INT   NOT NULL DEFAULT 0,
	[TypeId] INT NOT NULL,
    [SettingValue] NVARCHAR (MAX)  DEFAULT ('') NOT NULL,
	[IsLocked] BIT  DEFAULT ((0)) NOT NULL,
    [IsInternalOnly] BIT DEFAULT ((0)) NOT NULL,
    [Description]  NVARCHAR (1000) NULL,
    [ModifiedDate] DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL,
    CONSTRAINT [PK_AppSetting_Audit] PRIMARY KEY CLUSTERED ([AuditId] ASC)
);


