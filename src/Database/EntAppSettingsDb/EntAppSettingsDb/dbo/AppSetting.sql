CREATE TABLE [dbo].[AppSetting] (
    [AppSettingId]     INT             IDENTITY (1, 1) NOT NULL,
    [SettingKey]       NVARCHAR (50)   NOT NULL,
    [TenantId]         INT             DEFAULT ((0)) NOT NULL,
    [SettingGroupId]   INT             DEFAULT ((0)) NOT NULL,
    [SettingSectionId] INT             DEFAULT ((0)) NOT NULL,
    [TypeId]           INT             NOT NULL,
    [SettingValue]     NVARCHAR (MAX)  DEFAULT ('') NOT NULL,
    [IsLocked]         BIT             DEFAULT ((0)) NOT NULL,
    [IsInternalOnly]   BIT             DEFAULT ((0)) NOT NULL,
    [Description]      NVARCHAR (1000) NULL,
    [ModifiedDate]     DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL,
    CONSTRAINT [PK_AppSetting] PRIMARY KEY CLUSTERED ([AppSettingId] ASC),
    CONSTRAINT [FK_AppSetting_AppSettingGroup] FOREIGN KEY ([SettingGroupId]) REFERENCES [dbo].[AppSettingGroup] ([AppSettingGroupId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_AppSetting_AppSettingSection] FOREIGN KEY ([SettingSectionId]) REFERENCES [dbo].[AppSettingSection] ([AppSettingSectionId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_AppSetting_AppSettingType] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[AppSettingType] ([AppSettingTypeId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_AppSetting_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenant] ([TenantId]) ON DELETE CASCADE ON UPDATE CASCADE
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
