-- =============================================
-- Author:		Nicholas Rogoff
-- Create date: 07/09/2015
-- Description:	Clears down the Audit tables
-- =============================================
CREATE PROCEDURE ClearDownAuditTables 
	-- Add the parameters for the stored procedure here
	@keep_months int = 6
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @audit_table_name NVARCHAR(255);
	DECLARE @sql NVARCHAR(4000)

    -- Get list of Audit Tables
	DECLARE audit_tables_cursor CURSOR
	FOR
	select [name] from [sys].[tables] where sys.tables.[name] like '%_Audit' and sys.tables.[type] = 'U';

	OPEN audit_tables_cursor;
	
	--Fetch first
	FETCH NEXT FROM audit_tables_cursor INTO @audit_table_name

	WHILE @@FETCH_STATUS = 0
	BEGIN

		SELECT @sql = 'DELETE FROM [dbo].[' + @audit_table_name + '] WHERE [ModifiedDate] < DATEADD(month,-' + CAST(@keep_months AS NVARCHAR) + ', SYSUTCDATETIME())';
		EXEC(@sql);
		--get next row
		FETCH NEXT FROM audit_tables_cursor INTO @audit_table_name;
	END
	CLOSE audit_tables_cursor;
	
	DEALLOCATE audit_tables_cursor;

END
GO

