/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO [dbo].[AppSettingType](
	[AppSettingType], 
    [AppSettingTypeDescription])
VALUES
	('HMTL','HTML Content'),
	('JSON','JSON Content'),
	('XML','XML Content'),
	('TEXT','Text Content'),
	('NUMERIC','Numbers only Content. Integers and floats')
GO

INSERT INTO [dbo].[AppSettingType](
	[AppSectionId],
	[Section], 
    [Description])
VALUES
	(0, 'General','Platform general settings')
GO

INSERT INTO [dbo].[AppSettingGroup](
	[AppSettingGroupId],
	[Group],
	[Description])
VALUES
	(0, 'Core', 'Settings apply to ALL consumers and is the root of all inherited settings.')
GO

INSERT INTO [dbo].[AppSetting](
	[SettingKey],
	[SettingGroupId],
	[SettingSectionId],
	[Type],
	[SettingValue],
	[Description])
VALUES
	('TestSetting',0,0,'TEXT','This is a test text setting','Remove this test setting')
GO

INSERT INTO [dbo].[Tenant](
	[TenantId],[TenantName],[TenantCode],[TenantDescription])
	VALUES
		(0,'Platform','PLATFORM','Applies to all tenants')
GO
