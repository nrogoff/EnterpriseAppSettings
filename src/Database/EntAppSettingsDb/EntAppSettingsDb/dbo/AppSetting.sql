CREATE TABLE [dbo].[AppSetting] (
	[AppSettingId] INT NOT NULL IDENTITY, 
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
    CONSTRAINT [PK_AppSetting] PRIMARY KEY ([AppSettingId]), 
    CONSTRAINT [FK_AppSetting_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Tenant]([TenantId]), 
    CONSTRAINT [FK_AppSetting_AppSettingGroup] FOREIGN KEY ([SettingGroupId]) REFERENCES [AppSettingGroup]([AppSettingGroupId]),
    CONSTRAINT [FK_AppSetting_AppSettingSection] FOREIGN KEY ([SettingSectionId]) REFERENCES [AppSettingSection]([AppSettingSectionId]),
    CONSTRAINT [FK_AppSetting_AppSettingType] FOREIGN KEY ([TypeId]) REFERENCES [AppSettingType]([AppSettingTypeId]),
);

GO


CREATE TRIGGER [dbo].[Trigger_AppSetting_Audit]
    ON [dbo].[AppSetting]
    FOR DELETE, UPDATE
    AS
    BEGIN
        SET NoCount ON;
		INSERT INTO dbo.AppSetting_Audit ([AppSettingId],[SettingKey],[TenantId],[SettingGroupId],[SettingSectionId],[TypeId],[SettingValue],[IsLocked], [IsInternalOnly], [Description], [ModifiedDate], [ModifiedBy])
		SELECT [AppSettingId],[SettingKey],[TenantId],[SettingGroupId],[SettingSectionId],[TypeId],[SettingValue],[IsLocked], [IsInternalOnly], [Description], [ModifiedDate], [ModifiedBy] FROM deleted
    END



GO

CREATE UNIQUE INDEX [UX_AppSetting_SettingKey_TenantIdGroupId_SectionId] ON [dbo].[AppSetting] ([SettingKey], [TenantId], [SettingGroupId], [SettingSectionId])
