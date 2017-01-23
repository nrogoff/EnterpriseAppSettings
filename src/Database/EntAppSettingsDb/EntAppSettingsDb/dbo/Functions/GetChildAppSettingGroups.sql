--Returns the list of Child Group Ids for a given Group Id
CREATE FUNCTION [dbo].[GetChildAppSettingGroups]
(
    @AppSettingGroupId varchar(50)
)
RETURNS @Result TABLE ([Id] varchar(50), [ParentGroupId] varchar(50)) AS BEGIN


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
