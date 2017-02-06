--Returns the list of Parent Group Ids for a given Group Id
CREATE FUNCTION [dbo].[GetParentAppSettingSections] 
(    
    @AppSettingSectionId INT
)
RETURNS @Result TABLE  ([Id] INT, [ParentSectionId] INT)
AS
    BEGIN

    WITH Result ([Id],[ParentSectionId])
    AS
    (
        SELECT [AppSettingSectionId],[ParentSectionId]
        FROM [dbo].[AppSettingSection] 
        WHERE [AppSettingSectionId] = @AppSettingSectionId
        UNION ALL
        SELECT S.[AppSettingSectionId], S.[ParentSectionId] FROM [dbo].[AppSettingSection] S
        INNER JOIN Result R ON S.[AppSettingSectionId] = R.[ParentSectionId]
    )
    INSERT INTO @Result
    SELECT [Id],[ParentSectionId]
    FROM Result

    RETURN
	  
END