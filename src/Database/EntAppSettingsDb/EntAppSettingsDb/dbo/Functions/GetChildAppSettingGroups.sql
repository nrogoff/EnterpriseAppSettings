--Returns the list of Child Group Ids for a given Group Id
CREATE FUNCTION [dbo].[GetChildAppSettingGroups]
(
    @AppSettingGroupId INT
)
RETURNS @Result TABLE ([Id] INT, [ParentGroupId] INT) AS BEGIN


    WITH Result ([AppSettingGroupId], [ParentGroupId])
    AS
    (
        SELECT [AppSettingGroupId], [ParentGroupId]
        FROM [dbo].[AppSettingGroup]
        WHERE [AppSettingGroupId] = @AppSettingGroupId
        UNION ALL
        SELECT G.[AppSettingGroupId], G.[ParentGroupId] FROM [dbo].[AppSettingGroup] G
        INNER JOIN Result R ON G.[ParentGroupId] = R.[AppSettingGroupId]
    )
    INSERT INTO @Result
    SELECT [AppSettingGroupId], [ParentGroupId]
    FROM Result

    RETURN

END
