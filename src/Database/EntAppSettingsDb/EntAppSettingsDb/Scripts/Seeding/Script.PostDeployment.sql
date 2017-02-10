
--Post-Deployment Script Template							
----------------------------------------------------------------------------------------
-- This file contains SQL statements that will be appended to the build script.		
-- Use SQLCMD syntax to include a file in the post-deployment script.			
-- Example:      :r .\myfile.sql								
-- Use SQLCMD syntax to reference a variable in the post-deployment script.		
-- Example:      :setvar TableName MyTable							
--               SELECT * FROM [$(TableName)]					
----------------------------------------------------------------------------------------

INSERT INTO [dbo].[AppSettingType](
	[AppSettingType], 
    [AppSettingTypeDescription])
VALUES
	('TEXT','Text Content'),
	('INTEGER', 'Integer (whole number) only'),
	('DECIMAL','Numbers that are not integers'),
	('BOOL', 'Boolean content (true = 1, false = 0)'),
	('CSV','Comma Seperated List. Use double quotes for fields that require it.'),
	('HMTL','HTML Content'),
	('JSON','JSON Content'),
	('XML','XML Content')
GO

SET IDENTITY_INSERT [dbo].[AppSettingSection] ON
GO
INSERT INTO [dbo].[AppSettingSection](
	[AppSettingSectionId],
	[Section],
	[Description])
VALUES
	(1, 'General','Platform general settings')
GO
SET IDENTITY_INSERT [dbo].[AppSettingSection] OFF
GO

SET IDENTITY_INSERT [dbo].[AppSettingGroup] ON
GO
INSERT INTO [dbo].[AppSettingGroup](
	[AppSettingGroupId],
	[Group],
	[Description])
VALUES
	(1, 'Core', 'Settings apply to ALL consumers and is the root of all inherited settings.')
GO
SET IDENTITY_INSERT [dbo].[AppSettingGroup] OFF
GO

SET IDENTITY_INSERT [dbo].[Tenant] ON
GO
INSERT INTO [dbo].[Tenant](
	[TenantId],[TenantName],[TenantCode],[TenantDescription])
	VALUES
		(1,'Platform','PLATFORM','Applies to all tenants')
GO
SET IDENTITY_INSERT [dbo].[Tenant] OFF
GO

INSERT INTO [dbo].[AppSetting](
	[SettingKey],
	[SettingGroupId],
	[SettingSectionId],
	[TypeId],
	[TenantId],
	[SettingValue],
	[Description])
VALUES
	('TestSetting',1,1,1,1,'This is a test text setting value','Description: Remove this test setting')
GO

