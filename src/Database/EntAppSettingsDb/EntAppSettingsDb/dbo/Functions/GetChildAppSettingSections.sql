--Returns the list of Child Section Ids for a given Section Id
CREATE FUNCTION [dbo].[GetChildAppSettingSections]
(
    @AppSettingSectionId INT
)
RETURNS @Result TABLE ([Id] INT, [ParentSectionId] INT) AS BEGIN


    WITH Result ([AppSettingSectionId], [ParentSectionId])
    AS
    (
        SELECT [AppSettingSectionId], [ParentSectionId]
        FROM [dbo].[AppSettingSection]
        WHERE [AppSettingSectionId] = @AppSettingSectionId
        UNION ALL
        SELECT S.[AppSettingSectionId], S.[ParentSectionId] FROM [dbo].[AppSettingSection] S
        INNER JOIN Result R ON S.[ParentSectionId] = R.[AppSettingSectionId]
    )
    INSERT INTO @Result
    SELECT [AppSettingSectionId], [ParentSectionId]
    FROM Result

    RETURN

END
