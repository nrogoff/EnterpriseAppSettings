--Returns the list of Parent Group Ids for a given Group Id
CREATE FUNCTION [dbo].[GetParentAppSettingGroups] 
(    
    @AppSettingGroupId INT
)
RETURNS @Result TABLE  ([Id] INT, [ParentGroupId] INT)
AS
    BEGIN

    WITH Result ([Id],[ParentGroupId])
    AS
    (
        SELECT [AppSettingGroupId],[ParentGroupId]
        FROM [dbo].[AppSettingGroup] 
        WHERE [AppSettingGroupId] = @AppSettingGroupId
        UNION ALL
        SELECT G.[AppSettingGroupId], G.[ParentGroupId] FROM [dbo].[AppSettingGroup] G
        INNER JOIN Result R ON G.[AppSettingGroupId] = R.[ParentGroupId]
    )
    INSERT INTO @Result
    SELECT [Id],[ParentGroupId]
    FROM Result

    RETURN
	  
END