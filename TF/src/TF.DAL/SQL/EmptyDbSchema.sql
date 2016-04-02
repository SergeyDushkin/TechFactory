/* DROP ALL TABLES */
DECLARE
	@table_name nvarchar(200),
	@SQLText nvarchar(max);

DECLARE table_cursor CURSOR
	FOR SELECT NAME FROM sysobjects WHERE xtype = 'U' ORDER BY NAME

OPEN table_cursor

FETCH NEXT FROM table_cursor 
INTO @table_name
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @SQLText = 'DROP TABLE [' + @table_name + ']';
	EXEC(@SQLText);

	FETCH NEXT FROM table_cursor 
	INTO @table_name
END 
CLOSE table_cursor;
DEALLOCATE table_cursor;