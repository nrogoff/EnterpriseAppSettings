-- =============================================
-- Author:		Nicholas Rogoff
-- Create date: 01/02/2017
-- Description:	Function that generates the Group Path Name in the format \{grandparent}\{parent}\{child}
-- =============================================
CREATE FUNCTION GetGroupPath 
(	
	-- Add the parameters for the function here
	@AppSettingGroupId INT
)
RETURNS NVARCHAR(1000) 
AS
BEGIN
	-- Declare the return variable here
	DECLARE @groupPath AS NVARCHAR(1000)

	-- Add the T-SQL statements to compute the return value here
	DECLARE @GroupName AS NVARCHAR(50)

	DECLARE ITEM_CURSOR CURSOR SCROLL READ_ONLY STATIC
	FOR	SELECT g.[Group] FROM [dbo].[GetParentAppSettingGroups] (@AppSettingGroupId) gid
		left join [dbo].[AppSettingGroup] g on gid.Id = g.AppSettingGroupId

	OPEN ITEM_CURSOR

	FETCH LAST FROM ITEM_CURSOR INTO @GroupName
	WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @groupPath = CONCAT(@groupPath,'\',@GroupName)
			FETCH PRIOR FROM ITEM_CURSOR INTO @GroupName
		END
	CLOSE ITEM_CURSOR 
	DEALLOCATE ITEM_CURSOR

	-- Return the result of the function
	RETURN @groupPath

END