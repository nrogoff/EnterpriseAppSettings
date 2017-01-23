-- =============================================
-- Author:		Nicholas Rogoff
-- Create date: 07/05/2016
-- Description:	Creates a base set of AppSettings for a new Tenant based on an existing Tenant
-- =============================================
CREATE PROCEDURE CreateNewAppSettingsSet 
	-- Add the parameters for the stored procedure here
	@NewTenantId int = 0, 
	@TemplateTenantId int = 0 --copy settings from
AS
BEGIN

	IF (@NewTenantId IS NULL OR @NewTenantId=0)
		BEGIN
			PRINT 'ERROR: You must supply a NewTenantId other than zero.'
			RETURN(1)
		END
	IF (@TemplateTenantId IS NULL OR @TemplateTenantId=0)
		BEGIN
			PRINT 'ERROR: You must supply a TemplateTenantId other than zero.'
			RETURN(1)
		END
	
	IF (SELECT COUNT(*) FROM [dbo].[Tenant]
		WHERE [TenantId] = @NewTenantId) = 0
		BEGIN
			PRINT 'ERROR: New Tenant does not exist in Tenant table.'
			RETURN(1)
		END

	IF (SELECT COUNT(*) FROM [dbo].[Tenant]
		WHERE [TenantId] = @TemplateTenantId) = 0
		BEGIN
			PRINT 'ERROR: Template Tenant does not exist in Tenant table.'
			RETURN(1)
		END


	INSERT INTO [dbo].[AppSetting]
           ([SettingKey]
           ,[TenantId]
           ,[SettingValue]
           ,[SettingGroupId]
           ,[SettingSectionId]
           ,[TypeId]
           ,[IsLocked]
           ,[IsInternalOnly]
           ,[Description]
           ,[ModifiedDate]
           ,[ModifiedBy])   
	SELECT [SettingKey]
           ,@NewTenantId
           ,'!! TO BE CONFIGURED !!'
           ,[SettingGroupId]
           ,[SettingSectionId]
           ,[TypeId]
           ,[IsLocked]
           ,[IsInternalOnly]
           ,[Description]
           ,GETUTCDATE()
           ,SUSER_NAME()
	FROM [dbo].[AppSetting]
	WHERE [TenantId]=@TemplateTenantId

-- Check for SQL Server errors.
IF @@ERROR <> 0 
   BEGIN
      RETURN(2)
   END
ELSE
   BEGIN
     -- SUCCESS!!
	 RETURN(0)
   END
END
GO
