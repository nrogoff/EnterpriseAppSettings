--CLEAR OUT ALL TABLES
delete from [dbo].[AppSetting]
GO
DBCC CHECKIDENT ('AppSetting', RESEED, -1)
GO

delete from [dbo].[Tenant]
GO
DBCC CHECKIDENT ('Tenant', RESEED, -1)
GO

delete from [dbo].[AppSettingType]
GO
DBCC CHECKIDENT ('AppSettingType', RESEED, 0)
GO

delete from [dbo].[AppSettingSection]
GO
DBCC CHECKIDENT ('AppSettingSection', RESEED, -1)
GO

delete from [dbo].[AppSettingGroup]
GO
DBCC CHECKIDENT ('AppSettingGroup', RESEED, -1)

GO
DECLARE	@return_value int

EXEC	@return_value = [dbo].[ClearDownAuditTables]
		@keep_months = 0
SELECT	'Return Value' = @return_value
GO